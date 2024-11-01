using MediatR;

namespace Catalog.API.Endpoints;

public class CatalogServices
{
    public IMediator Mediator { get; set; }
    // public IIdentityService IdentityService { get; }
    public ILogger<CatalogServices> Logger { get; set; }

    public CatalogServices(IMediator mediator, ILogger<CatalogServices> logger)
    {
        Mediator = mediator;
        Logger = logger;
    }
}