using SimpleMarket.Orders.Domain.Entities;

namespace SimpleMarket.Orders.Api.Models;

public class RequestCheckoutDto
{
    public Guid CustomerId { get; set; }
    public int AddressId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public List<CheckoutProductDto>? Products { get; set; }
}