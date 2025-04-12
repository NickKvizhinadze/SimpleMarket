using MassTransit;

namespace SimpleMarket.Orders.Persistence.Saga;

public class OrderStateInstance : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = string.Empty;
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public string PaymentAccountId { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
}