using DotNetHelpers.Extentions;
using DotNetHelpers.Models;
using SimpleMarket.Carrier.Persistence.Data;
using SimpleMarket.Carrier.Domain.OrderAggregate;
using SimpleMarket.Carrier.Application.Orders.Models;

namespace SimpleMarket.Carrier.Application.Orders.Services;

public class OrderService : IOrderService
{
    private readonly CarrierDbContext _dbContext;

    public OrderService(CarrierDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Order>> Create(CreateOrderDto model, CancellationToken cancellationToken)
    {
        try
        {
            var order = new Order
            {
                CustomerId = model.CustomerId,
                ShippingAmount = model.ShippingAmount,
                ShippingAddress = model.ShippingAddress,
                CreateDate = DateTime.Now,
                State = OrderState.Pending,
                Items = model.Items.Select(p => new OrderItem
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList() ?? []
            };

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.SuccessResult().WithData(order);
        }
        catch (Exception ex)
        {
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<Order>();
        }
    }
}