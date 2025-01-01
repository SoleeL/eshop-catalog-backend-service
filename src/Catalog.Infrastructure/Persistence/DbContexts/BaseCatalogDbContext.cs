using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.DbContexts;

public abstract class BaseCatalogDbContext : DbContext
{
    protected BaseCatalogDbContext(DbContextOptions options) : base(options) { }

    public DbSet<BrandStateEntity> BrandState { get; set; }
    public DbSet<BrandEntity> Brand { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<ProductEntity> Product { get; set; }
    public DbSet<TypeEntity> Type { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BrandStateEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BrandEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TypeEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}