using Microsoft.EntityFrameworkCore;
using MassTransit;
using SimpleMarket.Carrier.Api.Diagnostics;
using SimpleMarket.Carrier.Api.Extensions;
using SimpleMarket.Carrier.Persistence.Data;


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