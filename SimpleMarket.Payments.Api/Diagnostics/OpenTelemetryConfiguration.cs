using System.Reflection;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SimpleMarket.Payments.Api.Models;

namespace SimpleMarket.Payments.Api.Diagnostics;

public static class OpenTelemetryConfiguration
{
    public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(nameof(OpenTelemetrySettings)).Get<OpenTelemetrySettings>();
        var serviceName = "SimpleMarket.Payments.Api";

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
                tracing.AddAspNetCoreInstrumentation()
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

        return builder;
    }
}