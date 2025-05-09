using System.Diagnostics.Metrics;

namespace SimpleMarket.Orders.Shared.Diagnostics;

public static class ApplicationDiagnostics
{
    private const string ServiceName = "SimpleMarket.Orders";
    private static readonly Meter _meter = new(ServiceName);

    private static readonly Counter<long> _ordersCreatedCounter = _meter.CreateCounter<long>("orders_created_total");

    private static double _totalPrice = 0;

    private static ObservableGauge<double> _totalOrdersGauge = _meter.CreateObservableGauge(
        name:"orders_price",
        observeValue: () => new Measurement<double>(_totalPrice),
        description: "Sum of all order prices");


    public static void AddNewOrder(double price)
    {
        _ordersCreatedCounter.Add(1);
        _totalPrice += price;
    }
    
}