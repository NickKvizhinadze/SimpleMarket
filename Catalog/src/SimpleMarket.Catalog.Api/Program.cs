using SimpleMarket.Catalog.Api.Extensions;
using SimpleMarket.Catalog.Api.Infrastructure.Data;
using SimpleMarket.Catalog.Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.AddServiceDefaults(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.AddNpgsqlDbContext<CatalogDbContext>("CatalogDb");


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
app.MapDefaultEndpoints();

app.Run();