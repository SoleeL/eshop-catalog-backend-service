using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface ITypeRepository
{
    Task AddAsync(TypeEntity typeEntity);
    Task<IEnumerable<TypeEntity>> GetAllAsync();
    Task<TypeEntity?> GetByIdAsync(Guid id);
    Task UpdateAsync(TypeEntity type);
    Task DeleteAsync(Guid id);
}