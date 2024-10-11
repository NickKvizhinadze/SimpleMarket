using DotNetHelpers.Models;
using SimpleMarket.Orders.Api.Domain;
using SimpleMarket.Orders.Api.Models;

namespace SimpleMarket.Orders.Api.Services;

public interface IOrdersService
{
    Task<Result<Order>> Checkout(RequestCheckoutDto model, CancellationToken cancellationToken);
}