using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace ServiceDefaults.Diagnostics;

public static class OpenTelemetryConfiguration
{
    public static IServiceCollection AddOpenTelemetryService(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceName = configuration["OpenTelemetrySettings:ServiceName"]!;
        var otelEndpoint = configuration["OpenTelemetrySettings:OtlpEndpoint"]!;
        var metricsName = configuration["OpenTelemetrySettings:MetricsName"]!;
        
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
                    .AddMeter(metricsName)
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