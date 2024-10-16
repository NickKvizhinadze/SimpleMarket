using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Payments.Api.Diagnostics;
using SimpleMarket.Payments.Api.Infrastructure.Data;
using SimpleMarket.Payments.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<PaymentsDbContext>(opts =>
    opts.UseNpgsql(configuration.GetConnectionString("PaymentsConnectionString")));

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

#region Open telemetry

builder.AddOpenTelemetry();
#endregion

#region Register Services
builder.Services.AddScoped<IPaymentService, PaymentService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();