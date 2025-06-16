using SimpleMarket.Carrier.Grpc.Orders;

namespace SimpleMarket.Orders.Api.Extensions;

public static class GrpcServiceExtensions
{
    public static WebApplicationBuilder AddCarrierGrpcService(this WebApplicationBuilder builder)
    {
        var aspNetCoreUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
        string? grpcServiceUrl = null;

        if (!string.IsNullOrWhiteSpace(aspNetCoreUrls))
        {
            var urls = aspNetCoreUrls.Split(";", StringSplitOptions.RemoveEmptyEntries);
            grpcServiceUrl = urls.FirstOrDefault();
        }

        if(!string.IsNullOrWhiteSpace(grpcServiceUrl)) 
            builder.Configuration["GrpcSettings:CarrierServiceUrl"] = grpcServiceUrl;

        builder.Services.AddGrpcClient<OrderServiceDefinition.OrderServiceDefinitionClient>(options =>
        {
            var carrierServiceUrl = builder.Configuration.GetSection("GrpcSettings:CarrierServiceUrl").Value;
            if (string.IsNullOrEmpty(carrierServiceUrl))
            {
                throw new InvalidOperationException("GrpcSettings:CarrierServiceUrl configuration is missing or invalid.");
            }
            options.Address = new Uri(carrierServiceUrl);
        });
        
        return builder;
    }
}