using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceDefaults;

public static class MasstransitExtensions
{
    public static void AddMasstransitService(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        services.AddMassTransit(o =>
        {
            var userName = configuration["RabbitMqSettings:UserName"]!;
            var password = configuration["RabbitMqSettings:Password"]!;
            
            o.AddConsumers(assembly);
    
            o.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });   
    }
}