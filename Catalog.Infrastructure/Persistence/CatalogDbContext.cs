using Catalog.Domain.Entities;
using Catalog.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure.Persistence;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IConfiguration configuration) : base(options) { }

    public DbSet<BrandEntity> Brand { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<ProductEntity> Product { get; set; }
    public DbSet<TypeEntity> Type { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // builder.HasPostgresExtension("vector");
        modelBuilder.ApplyConfiguration(new BrandEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TypeEntityTypeConfiguration());

        // Add the outbox table to this context
        // builder.UseIntegrationEventLogs();
        
        base.OnModelCreating(modelBuilder);
    }
}