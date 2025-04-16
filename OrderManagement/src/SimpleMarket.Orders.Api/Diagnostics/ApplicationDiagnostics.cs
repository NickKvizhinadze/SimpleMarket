using System.Diagnostics.Metrics;

namespace SimpleMarket.Orders.Api.Diagnostics;

public static class ApplicationDiagnostics
{
    public const string ServiceName = "SimpleMarket.Orders.Api";
    public static readonly Meter Meter = new (ServiceName);
    
    public static readonly Counter<long> OrdersCreatedCounter = Meter.CreateCounter<long>("orders.created");
}