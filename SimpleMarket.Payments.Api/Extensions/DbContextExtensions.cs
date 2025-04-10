using Microsoft.EntityFrameworkCore;
using SimpleMarket.Payments.Api.Infrastructure.Data;

namespace SimpleMarket.Payments.Api.Extensions;

public static class DbContextExtensions
{
    public static void Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
        db.Database.Migrate();
    }
}