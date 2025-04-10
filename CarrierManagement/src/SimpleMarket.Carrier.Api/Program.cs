using Microsoft.EntityFrameworkCore;
using MassTransit;
using Amazon.SQS;
using Amazon.SimpleNotificationService;
using SimpleMarket.Carrier.Api.Diagnostics;
using SimpleMarket.Carrier.Persistence.Data;
using SimpleMarket.Carrier.Persistence.Extensions;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CarrierDbContext>(opts =>
    opts.UseNpgsql(configuration.GetConnectionString("CarrierConnectionString")));

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

#endregion

#region OpenTelemetry

builder.AddOpenTelemetry();

#endregion

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