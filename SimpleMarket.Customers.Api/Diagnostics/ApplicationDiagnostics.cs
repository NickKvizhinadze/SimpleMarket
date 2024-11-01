using System.Diagnostics.Metrics;

namespace SimpleMarket.Customers.Api.Diagnostics;

public static class ApplicationDiagnostics
{
    private const string ServiceName = "SimpleMarket.Customers.Api";
    public static readonly Meter Meter = new (ServiceName);
    
    public static readonly Counter<long> OrdersCreatedCounter = Meter.CreateCounter<long>("customer.created");
}