using SimpleMarket.Payments.Api.Domain;

namespace SimpleMarket.Payments.Api.Models;

public class CreatePaymentDto
{
    public Guid CustomerId { get; set; }
    public Guid CorrelationId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}