namespace SimpleMarket.Customers.Api.Models;

public class OpenTelemetrySettings
{
    public required string JaegerUrl { get; set; }
    public required string OtlpEndpoint { get; set; }
}