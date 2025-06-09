using SimpleMarket.Carrier.Application;
using SimpleMarket.Carrier.Grpc.Services;
using SimpleMarket.Carrier.Persistence.Data;
using SimpleMarket.Carrier.Application.Orders.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults(typeof(ICarrierApplicationMarker).Assembly);

// Add services to the container.
builder.Services.AddGrpc();

builder.AddNpgsqlDbContext<CarrierDbContext>("OrdersDb");

#region Register Services

builder.Services.AddScoped<IOrderService, OrderService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<OrderGrpcService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();