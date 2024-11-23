using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly CatalogDbContext _catalogDbContext;
    private readonly ILogger<BrandRepository> _logger;

    public BrandRepository(CatalogDbContext catalogDbContext, ILogger<BrandRepository> logger)
    {
        _catalogDbContext = catalogDbContext;
        _logger = logger;
    }

    public async Task AddAsync(BrandEntity brandEntity)
    {
        _catalogDbContext.UsePrimaryConnection();
        await _catalogDbContext.Brand.AddAsync(brandEntity);
    }

    public async Task<(IEnumerable<BrandEntity>, int)> GetPageAsync(
        bool? enabled,
        Approval? approval,
        string? search,
        List<string> sort,
        int page,
        int size
    )
    {
        _catalogDbContext.UseReplicaConnection();
        IQueryable<BrandEntity> queryable = _catalogDbContext.Brand.AsNoTracking().AsQueryable();

        if (enabled != null) queryable = queryable.Where(b => b.Enabled == enabled);

        if (approval != null) queryable = queryable.Where(b => b.Approval == (int)approval);

        if (!string.IsNullOrEmpty(search))
        {
            queryable = queryable.Where(b =>
                b.Name.Contains(search) ||
                (b.Description != null && b.Description.Contains(search))
            );
        }

        if (sort.Any()) queryable = queryable.OrderByColumns(sort);

        int totalCount = await queryable.CountAsync();
        IEnumerable<BrandEntity> brands = await queryable
            .PageBy(page, size)
            .ToListAsync();

        _logger.LogInformation("Query expression: {QueryExpression}", queryable.ToQueryString());

        return (brands, totalCount);
    }

    public Task<BrandEntity?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(BrandEntity brand)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> BrandNameExistsAsync(string name)
    {
        _catalogDbContext.UseReplicaConnection();
        return await _catalogDbContext.Brand.AnyAsync(b => b.Name.ToLower() == name);
    }
}