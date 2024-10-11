using System.Text;
using System.Text.Json;
using DotNetHelpers.Extentions;
using DotNetHelpers.Models;
using SimpleMarket.Orders.Api.Models;

namespace SimpleMarket.Orders.Api.Services;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("PaymentServiceClient");
    }

    public async Task<Result> PayOrder(PayOrderDto request, CancellationToken cancellationToken)
    {
        var response =
            await _httpClient.PostAsync("api/Payments", ConvertToStringContent(request), cancellationToken);
        if (!response.IsSuccessStatusCode)
            return Result.BadRequestResult().WithError($"error with status {response.StatusCode}");

        return Result.SuccessResult();
    }

    #region Private Methods

    private StringContent ConvertToStringContent<T>(T content)
        => new(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

    #endregion
}