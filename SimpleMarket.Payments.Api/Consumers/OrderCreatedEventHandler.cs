using System.Diagnostics;
using System.Text.Json;
using MassTransit;
using OpenTelemetry;
using SimpleMarket.Orders.Contracts;
using SimpleMarket.Payments.Api.Models;
using SimpleMarket.Payments.Api.Services;

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

        var orderId = Baggage.Current.GetBaggage("order.id");
        Activity.Current?.AddTag("order.id", orderId);

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