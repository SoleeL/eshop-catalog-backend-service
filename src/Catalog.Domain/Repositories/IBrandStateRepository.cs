using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IBrandStateRepository
{
    Task AddWithSaveChange(BrandStateEntity brandStateEntity, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<BrandStateEntity>> GetAll(CancellationToken cancellationToken);

    Task<BrandStateEntity?> GetById(int id, CancellationToken cancellationToken = default);

    Task<BrandStateEntity?> UpdateWithSaveChange(
        int id,
        string? name,
        string? description,
        CancellationToken cancellationToken = default);

    Task<BrandStateEntity?> DeleteWithSaveChange(int id, CancellationToken cancellationToken = default);

    // Internal utils
    Task<bool> NameExists(string name);
    Task<bool> StateExists(int id);
}