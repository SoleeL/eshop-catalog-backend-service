using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

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
        await this._catalogDbContext.Brand.AddAsync(brandEntity);
    }
    
    public Task<IEnumerable<BrandEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
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