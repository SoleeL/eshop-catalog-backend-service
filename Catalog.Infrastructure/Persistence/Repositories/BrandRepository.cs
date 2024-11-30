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
    private readonly CatalogReplicaDbContext _catalogReplicaDbContext;
    private readonly ILogger<BrandRepository> _logger;

    public BrandRepository(
        CatalogPrimaryDbContext catalogPrimaryDbContext,
        CatalogReplicaDbContext catalogReplicDbContext,
        ILogger<BrandRepository> logger
    )
    {
        _catalogPrimaryDbContext = catalogPrimaryDbContext;
        _catalogReplicaDbContext = catalogReplicDbContext;
        _logger = logger;
    }

    public async Task AddWithSaveChange(BrandEntity brandEntity, CancellationToken cancellationToken = default)
    {
        await _catalogPrimaryDbContext.Brand.AddAsync(brandEntity, cancellationToken);
        await _catalogPrimaryDbContext.SaveChangesAsync(cancellationToken);
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
        IQueryable<BrandEntity> queryable = _catalogReplicaDbContext.Brand.AsNoTracking().AsQueryable();

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

    public Task<BrandEntity?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return _catalogReplicaDbContext.Brand.AsNoTracking().FirstOrDefaultAsync(
            b => b.Id == guid,
            cancellationToken);
    }

    public async Task<BrandEntity?> UpdateWithSaveChange(
        Guid guid,
        string? name,
        string? description,
        bool? enabled,
        Approval? approval,
        CancellationToken cancellationToken
    )
    {
        BrandEntity? brandEntity = await _catalogReplicaDbContext.Brand.FindAsync(guid, cancellationToken);

        if (brandEntity == null) return null;

        if (name != null) brandEntity.Name = name;
        if (description != null) brandEntity.Description = description;
        if (enabled != null) brandEntity.Enabled = (bool)enabled;
        if (approval != null) brandEntity.Approval = (int)approval;

        _catalogPrimaryDbContext.Brand.Update(brandEntity);
        await _catalogPrimaryDbContext.SaveChangesAsync(cancellationToken);
        return brandEntity;
    }

    public async Task<BrandEntity?> UpdateAsync(
        Guid guid,
        string? name,
        string? description,
        bool? enabled,
        Approval? approval
    )
    {
        BrandEntity? brandEntity = await _catalogReplicaDbContext.Brand.FindAsync(guid);

        if (brandEntity == null) return null;

        if (name != null) brandEntity.Name = name;
        if (description != null) brandEntity.Description = description;
        if (enabled != null) brandEntity.Enabled = (bool)enabled;
        if (approval != null) brandEntity.Approval = (int)approval;

        _catalogPrimaryDbContext.Brand.Update(brandEntity); // ESTO NO ACTUALIZA AUTOMATICAMENTE EN LA DB
        return brandEntity;
    }

    public async Task<BrandEntity?> DeleteWithSaveChange(Guid guid, CancellationToken cancellationToken = default)
    {
        // README: Quizas usar un AsNoTracking().FirstOrDefaultAsync()
        BrandEntity? brandEntity = await _catalogReplicaDbContext.Brand.FindAsync(guid, cancellationToken);

        if (brandEntity == null) return null;

        _catalogPrimaryDbContext.Brand.Remove(brandEntity); // No es asincrono
        await _catalogPrimaryDbContext.SaveChangesAsync(cancellationToken);

        return brandEntity;
    }

    public async Task<BrandEntity?> DeleteAsync(Guid guid)
    {
        BrandEntity? brandEntity = await _catalogReplicaDbContext.Brand.FindAsync(guid);

        if (brandEntity == null) return null;

        _catalogPrimaryDbContext.Brand.Remove(brandEntity);
        return brandEntity;
    }

    public async Task<bool> BrandNameExistsAsync(string name)
    {
        return await _catalogReplicaDbContext.Brand.AnyAsync(b => b.Name.ToLower() == name);
    }
}