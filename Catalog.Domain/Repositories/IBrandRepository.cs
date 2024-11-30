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

    Task<BrandEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(BrandEntity brand);

    Task<BrandEntity?> DeleteWithSaveChange(Guid id, CancellationToken cancellationToken = default);
    
    Task<BrandEntity?> DeleteAsync(Guid id);
    
    // Internal utils
    Task<bool> BrandNameExistsAsync(string name);
}