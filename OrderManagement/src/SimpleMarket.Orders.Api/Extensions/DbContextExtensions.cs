using Microsoft.EntityFrameworkCore;
using SimpleMarket.Orders.Persistence.Data;

namespace SimpleMarket.Orders.Api.Extensions;

public static class DbContextExtensions
{
    public static void Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
        db.Database.Migrate();
    }
}