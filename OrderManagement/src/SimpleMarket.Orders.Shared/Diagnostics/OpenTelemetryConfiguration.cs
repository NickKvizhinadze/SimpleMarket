using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace SimpleMarket.Orders.Shared.Diagnostics;

public static class OpenTelemetryConfiguration
{
    public static IServiceCollection AddOpenTelemetryService(this IServiceCollection services, string serviceName, string otelEndpoint)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService(serviceName)
                    .AddAttributes(new[]
                    {
                        new KeyValuePair<string, object>("service.version",
                            Assembly.GetExecutingAssembly().GetName().Version!.ToString()),
                    });
            })
            .WithTracing(tracing =>
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddNpgsql()
                    .AddSource("MassTransit")
                    .AddMassTransitInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(options =>
                        options.Endpoint = new Uri(otelEndpoint)
                    )
            )
            .WithMetrics(metrics =>
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddMeter(ApplicationDiagnostics.Meter.Name)
                    .AddConsoleExporter()
                    .AddOtlpExporter(options =>
                        options.Endpoint = new Uri(otelEndpoint)
                    )
            )
            .WithLogging(logging => 
                logging.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(otelEndpoint);
                }));

        return services;
    }
}