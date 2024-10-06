using SimpleMarket.Orders.Api.Domain;

namespace SimpleMarket.Orders.Api.Models;

public class RequestCheckoutDto
{
    public Guid CustomerId { get; set; }
    public Guid AddressId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public List<CheckoutProductDto> Products { get; set; }
}