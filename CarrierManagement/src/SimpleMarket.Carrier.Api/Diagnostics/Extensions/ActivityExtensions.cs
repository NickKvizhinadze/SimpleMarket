using System.Diagnostics;
using SimpleMarket.Carrier.Domain.OrderAggregate;

namespace SimpleMarket.Carrier.Api.Diagnostics.Extensions;

public static class ActivityExtensions
{
   
    public static Activity? EnrichWithOrderData(this Activity? activity, Order order)
    {
        activity?.AddTag("order.id", order.Id);
        activity?.AddTag("order.customerId", order.CustomerId);
        activity?.AddTag("order.paymentMethod", order.PaymentMethod);
        activity?.AddTag("order.shippingAmount", order.ShippingAmount);

        return activity;
    }
}