using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure.Persistence;

public class CatalogDbContext : DbContext
{
    private readonly string _primaryConnectionString;
    private readonly string _replicaConnectionString;
    private string _currentConnectionString;

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, string primary, string replica) : base(options)
    {
        _primaryConnectionString = primary;
        _replicaConnectionString = replica;
        _currentConnectionString = _primaryConnectionString;
    }
    
    public void UsePrimaryConnection()
    {
        _currentConnectionString = _primaryConnectionString;
    }

    public void UseReplicaConnection()
    {
        _currentConnectionString = _replicaConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_currentConnectionString);
        }
    }
    
    public DbSet<BrandEntity> Brand { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<ProductEntity> Product { get; set; }
    public DbSet<TypeEntity> Type { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BrandEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TypeEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}