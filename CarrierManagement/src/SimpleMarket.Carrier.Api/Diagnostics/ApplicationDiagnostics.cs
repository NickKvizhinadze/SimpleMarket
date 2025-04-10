using System.Diagnostics.Metrics;

namespace SimpleMarket.Carrier.Api.Diagnostics;

public static class ApplicationDiagnostics
{
    private const string ServiceName = "SimpleMarket.Orders.Api";
    public static readonly Meter Meter = new (ServiceName);
    
    public static readonly Counter<long> OrdersCreatedCounter = Meter.CreateCounter<long>("orders.created");
}