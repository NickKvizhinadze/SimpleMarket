using System.Diagnostics;
using MassTransit;
using SimpleMarket.Orders.Contracts;
using SimpleMarket.SharedLibrary.Events;
using SimpleMarket.Orders.Persistence.Saga;

namespace SimpleMarket.Orders.Saga.Diagnostics.Extensions;

public static class ActivityExtensions
{
    public static Activity? EnrichWithOrderData(this Activity? activity,  BehaviorContext<OrderStateInstance,OrderCreated> context)
    {
        Activity.Current?.SetTag("messaging.system", "rabbitmq");
        Activity.Current?.SetTag("messaging.operation", "publish");
        Activity.Current?.SetTag("messaging.destination", "order-created");
        Activity.Current?.SetTag(EventConstants.EventIdHeaderKey, context.Headers.Get<string>(EventConstants.EventIdHeaderKey));
        Activity.Current?.SetTag("order.id", context.Message.OrderId);
        Activity.Current?.SetTag("order.customerId", context.Message.CustomerId);
        Activity.Current?.SetTag("order.totalAmount", context.Message.TotalAmount);
        Activity.Current?.SetTag("order.paymentMethod", context.Message.PaymentMethod);

        return activity;
    }
}