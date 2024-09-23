namespace SimpleMarket.Catalog.Api.Models;

public class ProductsListDto
{
    public ProductsListDto(List<ProductDto> products, int totalCount)
    {
        Products = products;
        TotalCount = totalCount;
    }

    public List<ProductDto> Products { get; set; }
    public int TotalCount { get; set; }
}