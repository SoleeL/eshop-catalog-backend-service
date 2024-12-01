using MediatR;

namespace Catalog.Application.Commands.Types;

public abstract class DeleteTypeCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteTypeCommand(Guid id)
    {
        Id = id;
    }
}