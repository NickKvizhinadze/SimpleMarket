using System.Text.Json;
using MassTransit;
using SimpleMarket.Catalog.Contracts;
using SimpleMarket.Orders.Api.Domain;
using SimpleMarket.Orders.Api.Infrastructure.Data;

namespace SimpleMarket.Orders.Api.Consumers;

public class ProductDeletedEventHandler : IConsumer<ProductDeletedEvent>
{
    private readonly ILogger<ProductDeletedEventHandler> _logger;
    private readonly OrdersDbContext _dbContext;

    public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger, OrdersDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
    {
        _logger.LogInformation(JsonSerializer.Serialize(context.Message));

        var message = context.Message;
        var product = await _dbContext.Products.FindAsync(message.Id);

        if (product == null)
            return;
            
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }
}