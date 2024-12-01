using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class BrandStateRepository : IBrandStateRepository
{
    private readonly CatalogPrimaryDbContext _catalogPrimaryDbContext;
    private readonly CatalogReplicaDbContext _catalogReplicaDbContext;
    private readonly ILogger<BrandStateRepository> _logger;

    public BrandStateRepository(CatalogPrimaryDbContext catalogPrimaryDbContext,
        CatalogReplicaDbContext catalogReplicaDbContext, ILogger<BrandStateRepository> logger)
    {
        _catalogPrimaryDbContext = catalogPrimaryDbContext;
        _catalogReplicaDbContext = catalogReplicaDbContext;
        _logger = logger;
    }

    public async Task AddWithSaveChange(BrandStateEntity brandStateEntity,
        CancellationToken cancellationToken = default)
    {
        await _catalogPrimaryDbContext.BrandState.AddAsync(brandStateEntity, cancellationToken);
        await _catalogReplicaDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<BrandStateEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _catalogReplicaDbContext.BrandState.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BrandStateEntity?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await _catalogReplicaDbContext.BrandState.AsNoTracking().FirstOrDefaultAsync(
            bs => bs.Id == id,
            cancellationToken);
    }

    public async Task<BrandStateEntity?> UpdateWithSaveChange(
        int id,
        string? name,
        string? description,
        CancellationToken cancellationToken
    )
    {
        BrandStateEntity? brandStateEntity = await _catalogReplicaDbContext.BrandState.FindAsync(
            id,
            cancellationToken);

        if (brandStateEntity == null) return null;

        if (name != null) brandStateEntity.Name = name;
        if (description != null) brandStateEntity.Description = description;

        _catalogPrimaryDbContext.BrandState.Update(brandStateEntity);
        await _catalogPrimaryDbContext.SaveChangesAsync(cancellationToken);
        return brandStateEntity;
    }

    public async Task<BrandStateEntity?> DeleteWithSaveChange(int id, CancellationToken cancellationToken = default)
    {
        BrandStateEntity? brandStateEntity = await _catalogReplicaDbContext.BrandState.FindAsync(
            id, cancellationToken);
        
        if (brandStateEntity == null) return null;
        
        _catalogPrimaryDbContext.BrandState.Remove(brandStateEntity);
        await _catalogReplicaDbContext.SaveChangesAsync(cancellationToken);
        return brandStateEntity;
    }

    public async Task<bool> NameExists(string name)
    {
        return await _catalogReplicaDbContext.BrandState.AnyAsync(bs => bs.Name.ToLower() == name);
    }
    
    public async Task<bool> StateExists(int id)
    {
        return await _catalogReplicaDbContext.BrandState.AnyAsync(bs => bs.Id == id);
    }
}