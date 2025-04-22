using System.Diagnostics;
using DotNetHelpers.Extentions;
using DotNetHelpers.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Orders.Application.Common.Extensions;
using SimpleMarket.Orders.Application.Orders.Models;
using SimpleMarket.Orders.Contracts;
using SimpleMarket.Orders.Domain.Entities;
using SimpleMarket.Orders.Persistence.Data;
using SimpleMarket.Orders.Shared.Diagnostics;
using SimpleMarket.SharedLibrary.Events;

namespace SimpleMarket.Orders.Application.Orders.Services;

public class OrdersService : IOrdersService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly OrdersDbContext _dbContext;

    public OrdersService(OrdersDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<Order>> Checkout(RequestCheckoutDto model, CancellationToken cancellationToken)
    {
        try
        {
            
            Activity.Current?.EnrichWithOrderRequestData(model);

            var order = new Order
            {
                CustomerId = model.CustomerId,
                Amount = model.Products!.Sum(p => p.Price * p.Quantity),
                State = OrderState.Pending,
                PaymentMethod = model.PaymentMethod,
                ShippingAmount = 0, // TODO: calculate by address
                CreateDate = DateTime.UtcNow,
                ShippingAddressId = model.AddressId,
                OrderItems = model.Products?.Select(p => new OrderItem
                {
                    ProductId = p.Id,
                    Quantity = p.Quantity,
                    Price = p.Price
                }).ToList() ?? []
            };

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);


            var totalAmount = order.OrderItems.Sum(x => x.Price * x.Quantity);
            ApplicationDiagnostics.AddNewOrder((double)totalAmount);
            
            Activity.Current?.EnrichWithOrderData(order);

            await PublishOrderCreated(order, totalAmount, cancellationToken);


            return Result.SuccessResult().WithData(order);
        }
        catch (Exception ex)
        {
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<Order>();
        }
    }

    public Task<List<Order>> GetOrders(CancellationToken cancellationToken)
    {
        return _dbContext.Orders.ToListAsync(cancellationToken);
    }
    
    #region Private Methods

    private async Task PublishOrderCreated(Order order, decimal totalAmount, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(new OrderCreated
        {
            CreatedAt = order.CreateDate,
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            TotalAmount = totalAmount,
            PaymentMethod = (Contracts.PaymentMethod)order.PaymentMethod
        }, context =>
        {
            var guid = Guid.NewGuid().ToString();
            context.Headers.Set(EventConstants.EventIdHeaderKey, guid);
            context.Headers.Set(EventConstants.EventTimeHeaderKey,
                DateTime.UtcNow.ToString(EventConstants.Formats.DateFormat));
        }, cancellationToken);
    }
    #endregion
}