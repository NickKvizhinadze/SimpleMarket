using AutoMapper;
using DotNetHelpers.Models;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Catalog.Api.Domain;
using SimpleMarket.Catalog.Api.Infrastructure.Data;
using SimpleMarket.Catalog.Api.Models;

namespace SimpleMarket.Catalog.Api.Services;

public class ProductsService: IProductsService
{
    private readonly IMapper _mapper;
    private readonly CatalogDbContext _dbContext;

    public ProductsService(IMapper mapper, CatalogDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
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
                return Result.Error<ProductDetailsDto>("product_not_found", "Product not found");

            return Result.Success(_mapper.Map<ProductDetailsDto>(product));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error<ProductDetailsDto>(ex.Message);
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

            return Result.Success(
                new ProductsListDto(_mapper.Map<List<ProductDto>>(product), count)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error<ProductsListDto>(ex.Message);
        }
    }

    public async Task<Result<Guid>> CreateProduct(ProductCreateDto model, CancellationToken cancellationToken)
    {
        try
        {
            var product = _mapper.Map<Product>(model);

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error<Guid>(ex.Message);
        }
    }

    public async Task<Result<Guid>> UpdateProduct(Guid id, ProductUpdateDto model,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (product is null)
                return Result.Error<Guid>("product_not_found", "Product not found");

            _mapper.Map(model, product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error<Guid>(ex.Message);
        }
    }
    
    public async Task<Result> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (product is null)
                return Result.Error("product_not_found", "Product not found");


            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error(ex.Message);
        }
    }
}