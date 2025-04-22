using Microsoft.EntityFrameworkCore;
using SimpleMarket.Orders.Persistence.Saga;

// ReSharper disable once CheckNamespace
namespace SimpleMarket.Orders.Persistence.Data;

public partial class OrdersDbContext
{
    public DbSet<OrderStateInstance> OrderStates { get; set; }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}