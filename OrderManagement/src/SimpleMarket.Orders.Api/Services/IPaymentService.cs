using DotNetHelpers.Models;
using SimpleMarket.Orders.Api.Models;

namespace SimpleMarket.Orders.Api.Services;

//TODO: remove payment with apis, it should be done with event sourcing
public interface IPaymentService
{
    Task<Result> PayOrder(PayOrderDto request, CancellationToken cancellationToken);
}