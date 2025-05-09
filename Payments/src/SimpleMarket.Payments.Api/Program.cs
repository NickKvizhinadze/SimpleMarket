using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Payments.Api.Diagnostics;
using SimpleMarket.Payments.Api.Extensions;
using SimpleMarket.Payments.Api.Infrastructure.Data;
using SimpleMarket.Payments.Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.AddServiceDefaults(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.AddNpgsqlDbContext<PaymentsDbContext>("PaymentsDb");

#region Register Services
builder.Services.AddScoped<IPaymentService, PaymentService>();
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
app.MapDefaultEndpoints();

app.Run();