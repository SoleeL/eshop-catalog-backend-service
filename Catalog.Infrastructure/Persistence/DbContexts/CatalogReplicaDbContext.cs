using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.DbContexts;

public class CatalogReplicaDbContext : BaseCatalogDbContext
{
    public CatalogReplicaDbContext(DbContextOptions<CatalogReplicaDbContext> options) : base(options) { }
}