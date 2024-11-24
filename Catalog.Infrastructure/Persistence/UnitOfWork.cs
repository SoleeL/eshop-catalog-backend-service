using Catalog.Domain;
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
        cancellationToken.ThrowIfCancellationRequested(); // Verificar si se canceló la request por el cliente o otro motivo
        // La OperationCanceledException no es tratada por defecto como una excepción grave o no controlada, por lo que
        // puede no llegar al manejador global de excepciones. Deberás capturarla explícitamente en tu código.
        
        _dbContextTransaction = await _catalogPrimaryDbContext.Database.BeginTransactionAsync(cancellationToken);
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested(); // Verificar si se canceló la request por el cliente o otro motivo
        await _catalogPrimaryDbContext.SaveChangesAsync(cancellationToken);
        await _dbContextTransaction.CommitAsync(cancellationToken);
        await _dbContextTransaction.DisposeAsync();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        // IMPORTANT: EL LA CANCELLATIONTOKEN MOLESTA AQUI, NO DEJA REALIZAR EL ROLLBACK
        // cancellationToken.ThrowIfCancellationRequested(); // Verificar si se canceló la request por el cliente o otro motivo
        await _dbContextTransaction.RollbackAsync(cancellationToken);
        await _dbContextTransaction.DisposeAsync();
    }
}