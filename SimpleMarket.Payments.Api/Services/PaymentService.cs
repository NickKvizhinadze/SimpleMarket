using DotNetHelpers.Models;
using SimpleMarket.Payments.Api.Domain;
using SimpleMarket.Payments.Api.Infrastructure.Data;
using SimpleMarket.Payments.Api.Models;

namespace SimpleMarket.Payments.Api.Services;

public class PaymentService: IPaymentService
{
    private readonly PaymentsDbContext _dbContext;

    public PaymentService(PaymentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> CreatePayment(CreatePaymentDto model, CancellationToken cancellationToken)
    {
        var payment = new Payment
        {
            CustomerId = model.CustomerId,
            CorrelationId = model.CorrelationId,
            PaymentMethod = model.PaymentMethod,
            Amount = model.Amount
        };

        await _dbContext.Payments.AddAsync(payment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result.SuccessResult();
    }
}