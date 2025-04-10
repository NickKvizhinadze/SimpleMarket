using SimpleMarket.Orders.Domain.Entities;

namespace SimpleMarket.Orders.Api.Models;

public class PayOrderDto
{
    public Guid CustomerId { get; set; }
    public Guid CorrelationId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}