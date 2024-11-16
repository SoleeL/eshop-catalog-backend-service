using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Idempotency;

public class RequestManager : IRequestManager
{
    private readonly CatalogDbContext _context;

    public RequestManager(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        var request = await _context.
            FindAsync<ClientRequest>(id);

        return request != null;
    }

    public async Task CreateRequestForCommandAsync<T>(Guid id)
    {
        var exists = await ExistAsync(id);

        if (exists)
        {
            return; // TODO Return null?
        }
        // var request = exists ?
        //     throw new OrderingDomainException($"Request with {id} already exists") :
        //     new ClientRequest()
        //     {
        //         Id = id,
        //         Name = typeof(T).Name,
        //         Time = DateTime.UtcNow
        //     };

        ClientRequest clientRequest = new ClientRequest()
        {
            Id = id,
            Name = typeof(T).Name,
            Time = DateTime.UtcNow
        };

        _context.Add(clientRequest);

        await _context.SaveChangesAsync();
    }
}
