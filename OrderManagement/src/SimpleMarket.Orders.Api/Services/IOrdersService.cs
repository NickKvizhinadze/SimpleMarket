using DotNetHelpers.Models;
using SimpleMarket.Orders.Api.Models;
using SimpleMarket.Orders.Domain.Entities;

namespace SimpleMarket.Orders.Api.Services;

public interface IOrdersService
{
    Task<Result<Order>> Checkout(RequestCheckoutDto model, CancellationToken cancellationToken);
    Task<List<Order>> GetOrders(CancellationToken cancellationToken);
}