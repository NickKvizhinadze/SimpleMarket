using MassTransit;
using SimpleMarket.Orders.Contracts;
using SimpleMarket.Orders.Domain.Entities;
using SimpleMarket.Orders.Persistence.Saga;
using System.Diagnostics;
using OpenTelemetry;
using SimpleMarket.Orders.Saga.Diagnostics;
using SimpleMarket.SharedLibrary.Events;
using SimpleMarket.Orders.Saga.Diagnostics.Extensions;

namespace SimpleMarket.Orders.Saga;

public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
{
    public State Created { get; set; }
    public State Pending { get; set; }
    public State Paid { get; set; }
    public State Shipped { get; set; }

    public State Completed { get; set; }
    public State Canceled { get; set; }

    public Event<OrderCreated> OrderCreated { get; set; }
    public Event<OrderPaid> OrderPaid { get; set; }
    public Event<OrderShipped> OrderShipped { get; set; }
    public Event<OrderCanceled> OrderCanceled { get; set; }
    public Event<OrderCompleted> OrderCompleted { get; set; }

    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreated, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderPaid, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderShipped, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderCanceled, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderCompleted, x => x.CorrelateById(context => context.Message.OrderId));

        Initially(
            When(OrderCreated)
                .Then(context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.CustomerId = context.Message.CustomerId;
                    context.Saga.CurrentState = OrderState.Pending.ToString();
                    context.Saga.CreatedDate = context.Message.CreatedAt;
                    context.Saga.TotalPrice = context.Message.TotalAmount;
                })
                .TransitionTo(Pending)
                .Publish(context =>
                {
                   Activity.Current?.EnrichWithOrderData(context);

                    return new OrderCreatedEvent()
                    {
                        OrderId = context.Message.OrderId,
                        CustomerId = context.Message.CustomerId,
                        Amount = context.Message.TotalAmount,
                        PaymentMethod = context.Message.PaymentMethod,
                    };
                })
        );

        During(Pending,
            When(OrderPaid)
                .Then(context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    //TODO: maybe need to log in otel
                })
                .TransitionTo(Paid),
            When(OrderCanceled)
                .Then(context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    //TODO: maybe need to log in otel
                })
                .TransitionTo(Canceled)
                .Publish(context => new OrderCanceledEvent
                {
                    OrderId = context.Message.OrderId
                })
        );

        During(Paid,
            When(OrderShipped)
                .Then(context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    //TODO: maybe need to log in otel
                })
                .TransitionTo(Shipped)
                .Publish(context => new OrderCanceledEvent
                {
                    OrderId = context.Message.OrderId
                }));

        DuringAny(When(OrderCompleted).Finalize());
        SetCompletedWhenFinalized();
    }
}
