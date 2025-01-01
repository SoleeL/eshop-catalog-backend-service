using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IBrandRepository
{
    Task AddWithSaveChange(BrandEntity brandEntity, CancellationToken cancellationToken = default);

    Task AddAsync(BrandEntity brandEntity);

    Task<(IEnumerable<BrandEntity>, int)> GetPageAsync(
        CancellationToken cancellationToken,
        bool? enabled,
        int? stateId,
        string? search,
        List<string> sort,
        int page,
        int size
    );

    Task<BrandEntity?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<BrandEntity?> UpdateWithSaveChange(
        Guid guid,
        string? name,
        string? description,
        bool? enabled,
        int? stateId,
        CancellationToken cancellationToken = default);

    Task<BrandEntity?> UpdateAsync(
        Guid guid,
        string? name,
        string? description,
        bool? enabled,
        int? stateId);

    Task<BrandEntity?> DeleteWithSaveChange(Guid guid, CancellationToken cancellationToken = default);

    Task<BrandEntity?> DeleteAsync(Guid guid);

    // Internal utils
    Task<bool> NameExists(string name);
}