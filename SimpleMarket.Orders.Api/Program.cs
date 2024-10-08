using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Orders.Api.Infrastructure.Data;
using SimpleMarket.Orders.Api.Services;

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

builder.Services.AddMassTransitHostedService();
#endregion

#region Register Services
builder.Services.AddScoped<IOrdersService, OrdersService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}