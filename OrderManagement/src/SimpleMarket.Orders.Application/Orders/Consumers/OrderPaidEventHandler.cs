using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Logging;
using SimpleMarket.Orders.Contracts;
using SimpleMarket.Orders.Domain.Entities;
using SimpleMarket.Orders.Persistence.Data;

namespace SimpleMarket.Orders.Application.Orders.Consumers;

public class OrderPaidEventHandler : IConsumer<OrderPaidEvent>
{
    private readonly ILogger<OrderPaidEventHandler> _logger;
    private readonly OrdersDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderPaidEventHandler(ILogger<OrderPaidEventHandler> logger, IPublishEndpoint publishEndpoint,
        OrdersDbContext dbContext)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<OrderPaidEvent> context)
    {
        _logger.LogInformation(JsonSerializer.Serialize(context.Message));

        var order = await _dbContext.Orders.FindAsync(context.Message.CorrelationId);

        if (order is null)
            return;

        order.State = OrderState.Purchased;
        await _dbContext.SaveChangesAsync();

        await _publishEndpoint.Publish(new OrderPaid
        {
            OrderId = context.Message.CorrelationId
        });
    }
}