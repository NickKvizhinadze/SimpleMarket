using Microsoft.EntityFrameworkCore;
using SimpleMarket.Carrier.Persistence.Data;

namespace SimpleMarket.Carrier.Api.Extensions;

public static class DbContextExtensions
{
    public static void Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CarrierDbContext>();
        db.Database.Migrate();
    }
}