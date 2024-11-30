using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Domain.Shared;

namespace Catalog.Domain.Repositories;

public interface IBrandRepository
{
    Task AddWithSaveChange(BrandEntity brandEntity, CancellationToken cancellationToken = default);

    Task AddAsync(BrandEntity brandEntity);

    Task<(IEnumerable<BrandEntity>, int)> GetPageAsync(
        CancellationToken cancellationToken,
        bool? enabled,
        Approval? approval,
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
        Approval? approval,
        CancellationToken cancellationToken = default);

    Task<BrandEntity?> UpdateAsync(
        Guid guid,
        string? name,
        string? description,
        bool? enabled,
        Approval? approval);

    Task<BrandEntity?> DeleteWithSaveChange(Guid guid, CancellationToken cancellationToken = default);

    Task<BrandEntity?> DeleteAsync(Guid guid);

    // Internal utils
    Task<bool> BrandNameExistsAsync(string name);
}