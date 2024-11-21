using Catalog.Application.Dtos;
using Catalog.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Domain.Repositories;
using Catalog.Domain.Shared;
using Catalog.Infrastructure.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly CatalogDbContext _catalogDbContext;

    public BrandRepository(CatalogDbContext catalogDbContext)
    {
        this._catalogDbContext = catalogDbContext;
    }

    public async Task AddAsync(BrandEntity brandEntity)
    {
        this._catalogDbContext.UsePrimaryConnection();
        await this._catalogDbContext.Brand.AddAsync(brandEntity);
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
        this._catalogDbContext.UseReplicaConnection();
        IQueryable<BrandEntity> queryable = this._catalogDbContext.Brand.AsNoTracking().AsQueryable();

        if (enabled != null) queryable = queryable.Where(b => b.Enabled == enabled);

        if (approval != null) queryable = queryable.Where(b => b.Approval == (int) approval);

        if (!string.IsNullOrEmpty(search))
        {
            queryable = queryable.Where(b =>
                b.Name.Contains(search) ||
                (b.Description != null && b.Description.Contains(search))
            );
        }

        if (sort.Any())
        {
            foreach (var sortField in sort)
            {
                if (sortField.StartsWith("-"))
                {
                    var field = sortField.Substring(1); // Remover el prefijo "-"
                    queryable = queryable.OrderByDescending(b => EF.Property<object>(b, field));
                }
                else
                {
                    queryable = queryable.OrderBy(u => EF.Property<object>(u, sortField));
                }
            }
        }

        int totalCount = await queryable.CountAsync();
        IEnumerable<BrandEntity> brands = await queryable
            .PageBy(page, size)
            .ToListAsync();

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
}