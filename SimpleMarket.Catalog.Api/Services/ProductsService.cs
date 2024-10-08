using AutoMapper;
using DotNetHelpers.Extentions;
using DotNetHelpers.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Catalog.Api.Domain;
using SimpleMarket.Catalog.Api.Infrastructure.Data;
using SimpleMarket.Catalog.Api.Models;
using SimpleMarket.Catalog.Contracts;

namespace SimpleMarket.Catalog.Api.Services;

public class ProductsService : IProductsService
{
    private readonly IMapper _mapper;
    private readonly CatalogDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductsService(IMapper mapper, CatalogDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<ProductDetailsDto>> GetProduct(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (product is null)
                return Result.BadRequestResult()
                    .WithError("Product not found", "product_not_found")
                    .WithEmptyData<ProductDetailsDto>();

            return Result.SuccessResult().WithData(_mapper.Map<ProductDetailsDto>(product));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<ProductDetailsDto>();
        }
    }

    public async Task<Result<ProductsListDto>> GetProducts(Paging paging, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _dbContext.Products
                .Skip(paging.PerPage * (paging.CurrentPage - 1))
                .Take(paging.CurrentPage)
                .ToListAsync(cancellationToken);

            var count = await _dbContext.Products.CountAsync(cancellationToken);

            return Result.SuccessResult().WithData(
                new ProductsListDto(_mapper.Map<List<ProductDto>>(product), count)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<ProductsListDto>();
        }
    }

    public async Task<Result<Guid>> CreateProduct(ProductCreateDto model, CancellationToken cancellationToken)
    {
        try
        {
            var product = _mapper.Map<Product>(model);

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await PublishUpdateEvent(product, cancellationToken);

            return Result.SuccessResult().WithData(product.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<Guid>();
        }
    }

    public async Task<Result<Guid>> UpdateProduct(Guid id, ProductUpdateDto model,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (product is null)
                return Result.BadRequestResult()
                    .WithError("Product not found", "product_not_found")
                    .WithEmptyData<Guid>();

            _mapper.Map(model, product);

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            await PublishUpdateEvent(product, cancellationToken);

            return Result.SuccessResult().WithData(product.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<Guid>();
        }
    }

    public async Task<Result> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (product is null)
                return Result.BadRequestResult()
                    .WithError("Product not found", "product_not_found");


            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            await PublishDeleteEvent(id, cancellationToken);
            
            return Result.SuccessResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult().WithError(ex.Message);
        }
    }
    
    #region Private Methods
    private async Task PublishUpdateEvent(Product product, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(new ProductUpdatedEvent
        {
            Id = product.Id,
            Title = product.Title,
            Price = product.Price
        }, cancellationToken);
    }
    
    private async Task PublishDeleteEvent(Guid id, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(new ProductDeletedEvent
        {
            Id = id
        }, cancellationToken);
    }
    #endregion
}