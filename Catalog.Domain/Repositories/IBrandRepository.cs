using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Domain.Shared;

namespace Catalog.Domain.Repositories;

public interface IBrandRepository
{
    Task AddAsync(BrandEntity brandEntity);

    Task<(IEnumerable<BrandEntity>, int)> GetPageAsync(
        bool? enabled,
        Approval? approval,
        string? search,
        List<string> sort,
        int page,
        int size
    );

    Task<BrandEntity?> GetByIdAsync(Guid id);

    Task UpdateAsync(BrandEntity brand);

    Task DeleteAsync(Guid id);
}