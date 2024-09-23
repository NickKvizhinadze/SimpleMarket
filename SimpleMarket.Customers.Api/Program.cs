using Microsoft.EntityFrameworkCore;
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


#region Register Services

builder.Services.AddScoped<ICustomerService, CustomersService>();
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
