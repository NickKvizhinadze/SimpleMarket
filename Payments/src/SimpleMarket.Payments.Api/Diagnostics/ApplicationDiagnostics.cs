using System.Diagnostics.Metrics;

namespace SimpleMarket.Payments.Api.Diagnostics;

public static class ApplicationDiagnostics
{
    public const string ServiceName = "SimpleMarket.Payments";
    public static readonly Meter Meter = new (ServiceName);
}