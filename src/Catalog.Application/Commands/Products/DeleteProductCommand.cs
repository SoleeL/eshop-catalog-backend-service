using MediatR;

namespace Catalog.Application.Commands.Products;

public abstract class DeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}