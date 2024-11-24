using Catalog.Application.Extensions;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.DbContexts;
using Catalog.Infrastructure.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly CatalogPrimaryDbContext _catalogPrimaryDbContext;
    private readonly CatalogReplicaDbContext _catalogReplicDbContext;
    private readonly ILogger<BrandRepository> _logger;

    public BrandRepository(
        CatalogPrimaryDbContext catalogPrimaryDbContext, 
        CatalogReplicaDbContext catalogReplicDbContext,
        ILogger<BrandRepository> logger
        )
    {
        _catalogPrimaryDbContext = catalogPrimaryDbContext;
        _catalogReplicDbContext = catalogReplicDbContext;
        _logger = logger;
    }

    public async Task AddAsync(BrandEntity brandEntity)
    {
        await _catalogPrimaryDbContext.Brand.AddAsync(brandEntity);
    }

    public async Task<(IEnumerable<BrandEntity>, int)> GetPageAsync(
        CancellationToken cancellationToken,
        bool? enabled,
        Approval? approval,
        string? search,
        List<string> sort,
        int page,
        int size
    )
    {
        IQueryable<BrandEntity> queryable = _catalogReplicDbContext.Brand.AsNoTracking().AsQueryable();

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

        // _logger.LogInformation("Query expression: {QueryExpression}", queryable.ToQueryString());
        
        // README: Si cancellationToken = true entonces la query no se ejecuta o EF intenta evitar que siga su ejecucion, y
        // dispara el OperationCanceledException por la cancelacion de la request
        int totalCount = await queryable.CountAsync(cancellationToken);
        IEnumerable<BrandEntity> brands = await queryable
            .PageBy(page, size)
            .ToListAsync(cancellationToken);
        
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
        return await _catalogReplicDbContext.Brand.AnyAsync(b => b.Name.ToLower() == name);
    }
}