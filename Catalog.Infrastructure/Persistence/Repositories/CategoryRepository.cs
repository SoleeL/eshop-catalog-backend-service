using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Persistence.Repositories;

public class CategoryRepository: ICategoryRepository
{
    public Task AddAsync(CategoryEntity categoryEntity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryEntity?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CategoryEntity category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}