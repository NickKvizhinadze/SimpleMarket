using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Carrier.Domain.OrderAggregate;

namespace SimpleMarket.Carrier.Persistence.Data;

public partial class CarrierDbContext : DbContext
{
    public CarrierDbContext(DbContextOptions<CarrierDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("carrier");
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}