using DotNetHelpers.Models;
using SimpleMarket.Payments.Api.Models;

namespace SimpleMarket.Payments.Api.Services;

public interface IPaymentService
{
    Task<Result> CreatePayment(CreatePaymentDto model, CancellationToken cancellationToken);
}