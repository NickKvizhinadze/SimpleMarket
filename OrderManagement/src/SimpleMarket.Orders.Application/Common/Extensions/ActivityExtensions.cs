using System.Diagnostics;
using SimpleMarket.Orders.Domain.Entities;
using SimpleMarket.Orders.Application.Orders.Models;

namespace SimpleMarket.Orders.Application.Common.Extensions;

public static class ActivityExtensions
{
    public static Activity? EnrichWithOrderRequestData(this Activity? activity, RequestCheckoutDto model)
    {
        activity?.AddTag("request.customerId", model.CustomerId);
        activity?.AddTag("request.paymentMethod", model.PaymentMethod);

        return activity;
    }
    
    public static Activity? EnrichWithOrderData(this Activity? activity, Order order)
    {
        activity?.AddTag("order.id", order.Id);
        activity?.AddTag("order.customerId", order.CustomerId);
        activity?.AddTag("order.paymentMethod", order.PaymentMethod);
        activity?.AddTag("order.amount", order.Amount);

        return activity;
    }
}