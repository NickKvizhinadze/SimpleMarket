namespace SimpleMarket.Orders.Contracts;

public class OrderPaidEvent
{
    public Guid CustomerId { get; set; }
    public Guid CorrelationId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}

public enum PaymentMethod
{
    BogCard = 1,
    BogLoyaltyBogCard = 2,
    BogInstallment = 3,
    Invoice = 4
}