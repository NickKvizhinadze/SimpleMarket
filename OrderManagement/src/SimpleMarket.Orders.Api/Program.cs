using SimpleMarket.Orders.Api.Extensions;
using SimpleMarket.Orders.Application;
using SimpleMarket.Orders.Persistence.Data;
using SimpleMarket.Orders.Application.Orders.Services;
using SimpleMarket.Orders.Application.Infrastructure.Services.Carrier;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(typeof(IOrdersApplicationMarker).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.AddNpgsqlDbContext<OrdersDbContext>("OrdersDb");

#region MassTransit

// builder.Services.AddMassTransit(o =>
// {
//     o.AddConsumers(typeof(Program).Assembly);
//     
//     o.UsingRabbitMq((context, cfg) =>
//     {
//         cfg.Host("localhost", "/", h =>
//         {
//             h.Username("guest");
//             h.Password("guest");
//         });
//
//         cfg.ConfigureEndpoints(context);
//     });
// });

#endregion

#region Register Services

builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<ICarrierClient, CarrierClient>();
builder.AddCarrierGrpcService();


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