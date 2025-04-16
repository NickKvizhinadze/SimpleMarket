﻿using System.Diagnostics.Metrics;

namespace SimpleMarket.Orders.Saga.Diagnostics;

public static class ApplicationDiagnostics
{
    public const string ServiceName = "SimpleMarket.Orders.Saga";
    public static readonly Meter Meter = new (ServiceName);
}