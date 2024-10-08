namespace SimpleMarket.Catalog.Contracts;

public class ProductUpdatedEvent
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
    
    public decimal Price { get; set; }
}