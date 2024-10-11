using DotNetHelpers.Models;
using SimpleMarket.Orders.Api.Models;

namespace SimpleMarket.Orders.Api.Services;

public interface IPaymentService
{
    Task<Result> PayOrder(PayOrderDto request, CancellationToken cancellationToken);
}