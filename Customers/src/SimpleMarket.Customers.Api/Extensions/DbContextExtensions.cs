using Microsoft.EntityFrameworkCore;
using SimpleMarket.Customers.Api.Infrastructure.Data;

namespace SimpleMarket.Customers.Api.Extensions;

public static class DbContextExtensions
{
    public static void Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CustomersDbContext>();
        db.Database.Migrate();
    }
}