using DotNetHelpers.Models;
using SimpleMarket.Catalog.Api.Models;

namespace SimpleMarket.Catalog.Api.Services;

public interface IProductsService
{
    Task<Result<ProductsListDto>> GetProducts(Paging paging, CancellationToken cancellationToken);
    Task<Result<ProductDetailsDto>> GetProduct(Guid id, CancellationToken cancellationToken);
    Task<Result<Guid>> CreateProduct(ProductCreateDto model, CancellationToken cancellationToken);
    Task<Result<Guid>> UpdateProduct(Guid id, ProductUpdateDto model, CancellationToken cancellationToken);
    Task<Result> DeleteProduct(Guid id, CancellationToken cancellationToken);
}