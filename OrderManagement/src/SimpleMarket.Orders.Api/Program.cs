using Microsoft.EntityFrameworkCore;
using MassTransit;
using Amazon.SQS;
using Amazon.SimpleNotificationService;
using SimpleMarket.Orders.Api.Models;
using SimpleMarket.Orders.Api.Services;
using SimpleMarket.Orders.Api.Diagnostics;
using SimpleMarket.Orders.Persistence.Data;
using SimpleMarket.Orders.Persistence.Extensions;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<OrdersDbContext>(opts =>
    opts.UseNpgsql(configuration.GetConnectionString("OrdersConnectionString")));

#region MassTransit

builder.Services.AddMassTransit(o =>
{
    o.AddConsumers(typeof(Program).Assembly);

    o.UsingAmazonSqs((context, cfg) =>
    {
        cfg.Host(new Uri("amazonsqs://localhost:4566"), h =>
        {
            h.AccessKey("simple-market");
            h.SecretKey("Paroli1!");
            h.Config(new AmazonSimpleNotificationServiceConfig { ServiceURL = "http://localhost:4566" });
            h.Config(new AmazonSQSConfig { ServiceURL = "http://localhost:4566" });
        });

        cfg.ConfigureEndpoints(context);
    });
});

#endregion

#region Register Services

builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

#endregion

#region OpenTelemetry

builder.AddOpenTelemetry();

#endregion

builder.Services.AddHttpClient("PaymentServiceClient", client =>
{
    var settings = builder.Configuration.GetSection(nameof(PaymentsSettings)).Get<PaymentsSettings>();
    client.BaseAddress = new Uri(settings!.BaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Migrate();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();