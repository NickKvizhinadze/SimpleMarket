using DotNetHelpers.Models;
using SimpleMarket.Carrier.Domain.OrderAggregate;
using SimpleMarket.Carrier.Application.Orders.Models;

namespace SimpleMarket.Carrier.Application.Orders.Services;

public interface IOrderService
{
    Task<Result<Order>> Create(CreateOrderDto model, CancellationToken cancellationToken);
}