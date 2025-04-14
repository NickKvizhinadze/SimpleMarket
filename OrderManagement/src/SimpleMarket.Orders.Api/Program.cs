using Microsoft.EntityFrameworkCore;
using MassTransit;
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


#region OpenTelemetry

builder.AddOpenTelemetry();

#endregion

#region MassTransit

builder.Services.AddMassTransit(o =>
{
    o.AddConsumers(typeof(Program).Assembly);
    
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
    app.Migrate();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();