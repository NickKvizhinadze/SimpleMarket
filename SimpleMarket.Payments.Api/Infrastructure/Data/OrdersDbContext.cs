using Microsoft.EntityFrameworkCore;
using SimpleMarket.Payments.Api.Domain;

namespace SimpleMarket.Payments.Api.Infrastructure.Data;

public class PaymentsDbContext : DbContext
{
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orders");
        base.OnModelCreating(modelBuilder);
    }
}