using System.Text.Json;
using MassTransit;
using SimpleMarket.Customers.Contracts;
using SimpleMarket.Orders.Domain.Entities;
using SimpleMarket.Orders.Persistence.Data;

namespace SimpleMarket.Orders.Api.Consumers;

public class CustomerUpdatedEventHandler : IConsumer<CustomerUpdatedEvent>
{
    private readonly ILogger<CustomerUpdatedEventHandler> _logger;
    private readonly OrdersDbContext _dbContext;

    public CustomerUpdatedEventHandler(ILogger<CustomerUpdatedEventHandler> logger, OrdersDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<CustomerUpdatedEvent> context)
    {
        _logger.LogInformation(JsonSerializer.Serialize(context.Message));

        
        var message = context.Message;
        var customer = await _dbContext.Customers.FindAsync(message.Id);

        if (customer == null)
        {
            customer = new Customer
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                PersonalNumber = message.PersonalNumber,
                PhoneNumber = message.PhoneNumber
            };
            _dbContext.Customers.Add(customer);
        }
        else
        {
            customer.Id = message.Id;
            customer.FirstName = message.FirstName;
            customer.LastName = message.LastName;
            customer.PersonalNumber = message.PersonalNumber;
            customer.PhoneNumber = message.PhoneNumber;
        }

        await _dbContext.SaveChangesAsync();
    }
}