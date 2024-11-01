using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface ICategoryRepository
{
    Task AddAsync(CategoryEntity categoryEntity);
    Task<IEnumerable<CategoryEntity>> GetAllAsync();
    Task<CategoryEntity?> GetByIdAsync(Guid id);
    Task UpdateAsync(CategoryEntity category);
    Task DeleteAsync(Guid id);
}