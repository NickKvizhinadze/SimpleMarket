using Microsoft.EntityFrameworkCore;
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


#region Register Services
builder.Services.AddScoped<IProductsService, ProductsService>();
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