using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _catalogDbContext;
    private IDbContextTransaction _dbContextTransaction;

    public UnitOfWork(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _dbContextTransaction = await _catalogDbContext.Database.BeginTransactionAsync(cancellationToken);
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
            await _dbContextTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await _dbContextTransaction.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            await _dbContextTransaction.DisposeAsync();
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        await _dbContextTransaction.RollbackAsync(cancellationToken);
        await _dbContextTransaction.DisposeAsync();
    }
}