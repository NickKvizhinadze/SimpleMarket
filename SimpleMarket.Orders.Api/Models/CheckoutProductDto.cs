namespace SimpleMarket.Orders.Api.Models;

public class CheckoutProductDto
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}