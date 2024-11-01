using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IBrandRepository
{
    Task AddAsync(BrandEntity brandEntity);
    Task<IEnumerable<BrandEntity>> GetAllAsync();
    Task<BrandEntity?> GetByIdAsync(Guid id);
    Task UpdateAsync(BrandEntity brand);
    Task DeleteAsync(Guid id);
}