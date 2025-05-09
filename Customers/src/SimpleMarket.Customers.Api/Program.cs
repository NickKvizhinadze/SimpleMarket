using SimpleMarket.Customers.Api.Services;
using SimpleMarket.Customers.Api.Extensions;
using SimpleMarket.Customers.Api.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.AddNpgsqlDbContext<CustomersDbContext>("CustomersDb");

#region Register Services

builder.Services.AddScoped<ICustomerService, CustomersService>();
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
