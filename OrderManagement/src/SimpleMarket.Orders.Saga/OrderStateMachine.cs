using MassTransit;
using SimpleMarket.Orders.Contracts;
using SimpleMarket.Orders.Persistence.Saga;

namespace SimpleMarket.Orders.Saga;

public class OrderStateMachine: MassTransitStateMachine<OrderStateInstance>
{
    public State Created { get; set; }
    public State Pending { get; set; }
    public State Canceled { get; set; }

    public Event<OrderCreated> OrderCreated { get; set; }
    public Event<OrderCompleted> OrderCompleted { get; set; }
    public Event<OrderCanceled> OrderCanceled { get; set; }
}