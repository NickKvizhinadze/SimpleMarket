using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Catalog.Api.Extensions;
using SimpleMarket.Catalog.Api.Infrastructure.Data;
using SimpleMarket.Catalog.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<CatalogDbContext>(opts =>
    opts.UseNpgsql(configuration.GetConnectionString("CatalogConnectionString")));

#region MassTransit

builder.Services.AddMassTransit(o =>
{
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
builder.Services.AddScoped<IProductsService, ProductsService>();
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