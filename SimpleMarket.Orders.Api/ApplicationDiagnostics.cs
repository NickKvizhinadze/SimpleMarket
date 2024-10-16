using System.Diagnostics;

public static class ApplicationDiagnostics
{
    public static ActivitySource ActivitySource = new ActivitySource(ActivitySourceName);
    
    public static string ActivitySourceName = "SimpleMarket.Orders.Diagnostics";
}