using System.Reflection;
using MassTransit.Logging;
using MassTransit.Monitoring;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using SimpleMarket.Orders.Api.Models;

namespace SimpleMarket.Orders.Api.Diagnostics;

public static class OpenTelemetryConfiguration
{
    public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(nameof(OpenTelemetrySettings)).Get<OpenTelemetrySettings>();
        var serviceName = "SimpleMarket.Orders.Api";

        builder.Services.AddOpenTelemetry()
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
                    .AddSource(DiagnosticHeaders.DefaultListenerName)
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
                logging.AddOtlpExporter(options => options.Endpoint = new Uri(settings!.OtlpEndpoint)),
                options =>
                {
                    options.IncludeFormattedMessage = true;
                });

        return builder;
    }
}