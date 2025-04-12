using Microsoft.Extensions.Hosting;
using MassTransit;
using Amazon.SQS;
using Amazon.SimpleNotificationService;
using SimpleMarket.Orders.Persistence.Data;
using SimpleMarket.Orders.Persistence.Saga;

namespace SimpleMarket.Orders.Saga;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(o =>
                {
                    o.SetEntityFrameworkSagaRepositoryProvider(r =>
                    {
                        r.ExistingDbContext<OrdersDbContext>();
                        r.UseSqlServer();
                    });
                    
                    o.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
                        .EntityFrameworkRepository(r =>
                        {
                            r.ExistingDbContext<OrdersDbContext>();
                            r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                            r.UsePostgres();
                        });

                    o.UsingAmazonSqs((context, cfg) =>
                    {
                        cfg.Host(new Uri("amazonsqs://localhost:4566"), h =>
                        {
                            h.AccessKey("simple-market");
                            h.SecretKey("Paroli1!");
                            h.Config(new AmazonSimpleNotificationServiceConfig
                                { ServiceURL = "http://localhost:4566" });
                            h.Config(new AmazonSQSConfig { ServiceURL = "http://localhost:4566" });
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });
            });
}