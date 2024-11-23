using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _catalogDbContext;
    private ILogger<UnitOfWork> _logger { get; }
    private IDbContextTransaction _dbContextTransaction;

    public UnitOfWork(CatalogDbContext catalogDbContext, ILogger<UnitOfWork> logger)
    {
        _catalogDbContext = catalogDbContext;
        _logger = logger;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested(); // Verificar si se canceló la request por el cliente o otro motivo
        // La OperationCanceledException no es tratada por defecto como una excepción grave o no controlada, por lo que
        // puede no llegar al manejador global de excepciones. Deberás capturarla explícitamente en tu código.
        
        // IMPORTANT: Las transacciones corresponden a procesos de modificacion, por lo que se tiene que usar la
        // conexion de escritura
        _catalogDbContext.UsePrimaryConnection();
        
        _dbContextTransaction = await _catalogDbContext.Database.BeginTransactionAsync(cancellationToken);
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested(); // Verificar si se canceló la request por el cliente o otro motivo
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
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