using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.DbContexts;

public class CatalogPrimaryDbContext : BaseCatalogDbContext
{
    public CatalogPrimaryDbContext(DbContextOptions<CatalogPrimaryDbContext> options) : base(options) { }
}