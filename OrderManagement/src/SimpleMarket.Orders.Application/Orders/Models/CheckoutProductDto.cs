namespace SimpleMarket.Orders.Application.Orders.Models;

public class CheckoutProductDto
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}