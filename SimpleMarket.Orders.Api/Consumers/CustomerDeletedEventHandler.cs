using System.Text.Json;
using MassTransit;
using SimpleMarket.Customers.Contracts;
using SimpleMarket.Orders.Api.Infrastructure.Data;

namespace SimpleMarket.Orders.Api.Consumers;

public class CustomerDeletedEventHandler : IConsumer<CustomerDeletedEvent>
{
    private readonly ILogger<CustomerDeletedEventHandler> _logger;
    private readonly OrdersDbContext _dbContext;

    public CustomerDeletedEventHandler(ILogger<CustomerDeletedEventHandler> logger, OrdersDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<CustomerDeletedEvent> context)
    {
        _logger.LogInformation(JsonSerializer.Serialize(context.Message));

        var message = context.Message;
        var customer = await _dbContext.Customers.FindAsync(message.Id);

        if (customer == null)
            return;
            
        _dbContext.Customers.Remove(customer);
        await _dbContext.SaveChangesAsync();
    }
}