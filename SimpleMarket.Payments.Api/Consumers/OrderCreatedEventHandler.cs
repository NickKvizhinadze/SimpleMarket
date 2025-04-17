using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using MassTransit;
using OpenTelemetry;
using SimpleMarket.Orders.Contracts;
using SimpleMarket.Payments.Api.Models;
using SimpleMarket.Payments.Api.Services;
using SimpleMarket.SharedLibrary.Events;

namespace SimpleMarket.Payments.Api.Consumers;

public class OrderCreatedEventHandler : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedEventHandler> _logger;
    private readonly IPaymentService _service;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IPaymentService service,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _service = service;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        _logger.LogInformation(JsonSerializer.Serialize(context.Message));

        using var activity = Activity.Current?.Source.StartActivity("Payments.ProcessOrderCreated", ActivityKind.Consumer);
        if (activity != null)
        {
            var cloudEventTime = context.Headers.Get<string>(EventConstants.EventTimeHeaderKey);
            if (!string.IsNullOrEmpty(cloudEventTime))
            {
                var publishTime = DateTime.Parse(cloudEventTime);

                var messageAge = DateTime.UtcNow - publishTime;

                activity.AddTag("messaging.message.age",
                    messageAge.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            }
            
            activity.SetTag("messaging.system", "rabbitmq");
            activity.SetTag("messaging.operation", "receive");
            activity.SetTag("messaging.destination", "order-created");
            activity.SetTag(EventConstants.EventIdHeaderKey, context.Headers.Get<string>(EventConstants.EventIdHeaderKey));
            activity.SetTag("order.id", context.Message.OrderId);
            activity.SetTag("order.customerId", context.Message.CustomerId);
            activity.SetTag("order.paymentMethod", context.Message.PaymentMethod);
        }

        var message = context.Message;
        var result = await _service.CreatePayment(new CreatePaymentDto
        {
            CorrelationId = message.OrderId,
            CustomerId = message.CustomerId,
            Amount = message.Amount,
            PaymentMethod = (SimpleMarket.Payments.Api.Domain.PaymentMethod)context.Message.PaymentMethod
        }, CancellationToken.None);

        if (result.Succeeded)
            await _publishEndpoint.Publish(new OrderPaidEvent
            {
                CorrelationId = message.OrderId,
            });
    }
}
