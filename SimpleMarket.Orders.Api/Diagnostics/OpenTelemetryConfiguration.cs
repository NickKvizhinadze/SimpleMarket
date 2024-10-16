using System.Reflection;
using Npgsql;
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
                    .AddConsoleExporter()
                    .AddOtlpExporter(options =>
                        options.Endpoint = new Uri(settings!.JaegerUrl)
                    )
            );

        return builder;
    }
}