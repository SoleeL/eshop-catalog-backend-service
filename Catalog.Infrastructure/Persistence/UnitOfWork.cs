using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _catalogDbContext;
    private ILogger<UnitOfWork> _logger { get; set; }
    private IDbContextTransaction _dbContextTransaction;

    public UnitOfWork(CatalogDbContext catalogDbContext, ILogger<UnitOfWork> logger)
    {
        _catalogDbContext = catalogDbContext;
        _logger = logger;
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
            await _dbContextTransaction.DisposeAsync();
        }
        catch (Exception exception) {
            await _dbContextTransaction.RollbackAsync(cancellationToken);
            _logger.LogInformation("CommitAsync exception: {ExceptionMessage}", exception.Message);
            _logger.LogInformation("CommitAsync exception: {ExceptionMessage}", exception.InnerException.Message);
            throw;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        await _dbContextTransaction.RollbackAsync(cancellationToken);
        await _dbContextTransaction.DisposeAsync();
    }
}