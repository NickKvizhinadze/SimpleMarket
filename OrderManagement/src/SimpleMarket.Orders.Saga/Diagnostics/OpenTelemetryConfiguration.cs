using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SimpleMarket.Orders.Saga.Models;

namespace SimpleMarket.Orders.Saga.Diagnostics;

public static class OpenTelemetryConfiguration
{
    public static IServiceCollection AddOpenTelemetryService(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(OpenTelemetrySettings)).Get<OpenTelemetrySettings>();
        var serviceName = "SimpleMarket.Orders.Api";

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
                    .AddMassTransitInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(options =>
                        options.Endpoint = new Uri(settings!.OtlpEndpoint)
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
                        options.Endpoint = new Uri(settings!.OtlpEndpoint)
                    )
                )
            .WithLogging(logging => 
                logging.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(settings!.OtlpEndpoint);
                }));

        return services;
    }
}