using Microsoft.EntityFrameworkCore;
using SimpleMarket.Catalog.Api.Domain;

namespace SimpleMarket.Catalog.Api.Infrastructure.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
}