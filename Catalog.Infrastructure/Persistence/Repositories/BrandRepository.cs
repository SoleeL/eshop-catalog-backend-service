using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly CatalogDbContext _catalogDbContext;

    public BrandRepository(CatalogDbContext catalogDbContext)
    {
        this._catalogDbContext = catalogDbContext;
    }

    public async Task AddAsync(BrandEntity brandEntity)
    {
        this._catalogDbContext.UsePrimaryConnection();
        await this._catalogDbContext.Brand.AddAsync(brandEntity);
    }
    
    public async Task<(IEnumerable<BrandEntity>, int)> GetPageAsync(int page, int size)
    {
        this._catalogDbContext.UseReplicaConnection();
        IQueryable<BrandEntity> queryable = this._catalogDbContext.Brand.AsNoTracking().AsQueryable();
        
        int totalCount = await queryable.CountAsync();
        IEnumerable<BrandEntity> brands = await queryable
            .PageBy(page, size)
            .ToListAsync();
        return (brands, totalCount);
    }

    public Task<BrandEntity?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(BrandEntity brand)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}