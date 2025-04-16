using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMarket.Orders.Persistence.Data;
using SimpleMarket.Orders.Persistence.Saga;
using SimpleMarket.Orders.Saga.Diagnostics;
using OpenTelemetry.Instrumentation.MassTransit;

namespace SimpleMarket.Orders.Saga;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                
                services.AddDbContext<OrdersDbContext>(opts =>
                    opts.UseNpgsql(hostContext.Configuration.GetConnectionString("OrdersConnectionString")));

                services.AddOpenTelemetryService(hostContext.Configuration);
                
                services.AddMassTransit(o =>
                {
                    o.SetEntityFrameworkSagaRepositoryProvider(r =>
                    {
                        r.ExistingDbContext<OrdersDbContext>();
                        r.UsePostgres();
                    });
                    
                    o.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
                        .EntityFrameworkRepository(r =>
                        {
                            r.ExistingDbContext<OrdersDbContext>();
                            r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                            r.UsePostgres();
                        });

                    o.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                        
                        cfg.ConfigureEndpoints(context);
                    });
                });
            });
}
