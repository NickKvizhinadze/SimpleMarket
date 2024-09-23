namespace SimpleMarket.Catalog.Api.Models;

public class ProductCreateDto
{
    public required string Title { get;  set; }
    
    public required string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int BrandId { get; set; }
    
    public int CategoryId { get; set; }
}