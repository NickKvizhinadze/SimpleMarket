using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Customers.Api.Diagnostics;
using SimpleMarket.Customers.Api.Infrastructure.Data;
using SimpleMarket.Customers.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<CustomersDbContext>(opts =>
    opts.UseNpgsql(configuration.GetConnectionString("CustomersConnectionString")));


#region MassTransit

builder.Services.AddMassTransit(o =>
{
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

builder.Services.AddScoped<ICustomerService, CustomersService>();
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
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
