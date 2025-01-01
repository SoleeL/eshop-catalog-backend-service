using Catalog.Domain;
using Catalog.Domain.Shared;
using Catalog.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogPrimaryDbContext _catalogPrimaryDbContext;
    private ILogger<UnitOfWork> _logger { get; }
    private IDbContextTransaction _dbContextTransaction;

    public UnitOfWork(CatalogPrimaryDbContext catalogPrimaryDbContext, ILogger<UnitOfWork> logger)
    {
        _catalogPrimaryDbContext = catalogPrimaryDbContext;
        _logger = logger;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _dbContextTransaction = await _catalogPrimaryDbContext.Database.BeginTransactionAsync(cancellationToken);
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _catalogPrimaryDbContext.SaveChangesAsync(cancellationToken);
        await _dbContextTransaction.CommitAsync(cancellationToken);
        await _dbContextTransaction.DisposeAsync();
    }

    public async Task RollbackAsync()
    {
        // README: No pasar el CancellationToken ya que interfiere con el rollback. Si se le pasa y el RollbackAsync
        // no se ejecuta, se podrian generar problemas de integridad de datos.
        await _dbContextTransaction.RollbackAsync();
        await _dbContextTransaction.DisposeAsync();
    }
}