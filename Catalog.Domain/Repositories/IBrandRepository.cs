using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IBrandRepository
{
    Task AddAsync(BrandEntity brandEntity);
    Task<(IEnumerable<BrandEntity>, int)>  GetPageAsync(int page, int size);
    Task<BrandEntity?> GetByIdAsync(Guid id);
    Task UpdateAsync(BrandEntity brand);
    Task DeleteAsync(Guid id);
}