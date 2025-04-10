using System.Text.Json;
using MassTransit;
using SimpleMarket.Catalog.Contracts;
using SimpleMarket.Orders.Domain.Entities;
using SimpleMarket.Orders.Persistence.Data;

namespace SimpleMarket.Orders.Api.Consumers;

public class ProductUpdatedEventHandler : IConsumer<ProductUpdatedEvent>
{
    private readonly ILogger<ProductUpdatedEventHandler> _logger;
    private readonly OrdersDbContext _dbContext;

    public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger, OrdersDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<ProductUpdatedEvent> context)
    {
        _logger.LogInformation(JsonSerializer.Serialize(context.Message));

        var message = context.Message;
        var product = await _dbContext.Products.FindAsync(message.Id);

        if (product == null)
        {
            product = new Product
            {
                Id = message.Id,
                Title = message.Title,
                Price = message.Price
            };
            _dbContext.Products.Add(product);
        }
        else
        {
            product.Title = message.Title;
            product.Price = message.Price;
        }

        await _dbContext.SaveChangesAsync();
    }
}