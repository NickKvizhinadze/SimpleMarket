using DotNetHelpers.Models;
using SimpleMarket.Orders.Domain.Entities;
using SimpleMarket.Orders.Application.Orders.Models;

namespace SimpleMarket.Orders.Application.Orders.Services;

public interface IOrdersService
{
    Task<Result<Order>> Checkout(RequestCheckoutDto model, CancellationToken cancellationToken);
    Task<List<Order>> GetOrders(CancellationToken cancellationToken);
}