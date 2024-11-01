using MediatR;

namespace Catalog.Application.Commands.Brands;

public class DeleteBrandCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteBrandCommand(Guid id)
    {
        Id = id;
    }
}