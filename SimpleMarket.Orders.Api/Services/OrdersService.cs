using AutoMapper;
using DotNetHelpers.Extentions;
using DotNetHelpers.Models;
using SimpleMarket.Orders.Api.Domain;
using SimpleMarket.Orders.Api.Infrastructure.Data;
using SimpleMarket.Orders.Api.Models;

namespace SimpleMarket.Orders.Api.Services;

public class OrdersService : IOrdersService
{
    private readonly IMapper _mapper;
    private readonly OrdersDbContext _dbContext;

    public OrdersService(IMapper mapper, OrdersDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Checkout(RequestCheckoutDto model)
    {
        try
        {
            var order = new Order
            {
                CustomerId = model.CustomerId,
                Amount = model.Products.Sum(p => p.Price * p.Quantity),
                State = OrderState.Pending,
                PaymentMethod = model.PaymentMethod,
                ShippingAmount = 0, // TODO: calculate by address
                CreateDate = DateTime.UtcNow,
                ShippingAddressId = model.AddressId
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return Result.SuccessResult().WithData(order.Id);
        }
        catch (Exception ex)
        {
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<Guid>();
        }
    }
}