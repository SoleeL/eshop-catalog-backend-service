using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Persistence.Repositories;

public class TypeRepository: ITypeRepository
{
    public Task AddAsync(TypeEntity typeEntity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TypeEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TypeEntity?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TypeEntity type)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}