using Microsoft.EntityFrameworkCore;
using SimpleMarket.Catalog.Api.Infrastructure.Data;

namespace SimpleMarket.Catalog.Api.Extensions;

public static class DbContextExtensions
{
    public static void Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        db.Database.Migrate();
    }
}