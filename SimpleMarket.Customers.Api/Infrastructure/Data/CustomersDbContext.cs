using Microsoft.EntityFrameworkCore;
using SimpleMarket.Customers.Api.Domain;

namespace SimpleMarket.Customers.Api.Infrastructure.Data;

public class CustomersDbContext : DbContext
{
    public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}