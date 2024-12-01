using MediatR;

namespace Catalog.Application.Commands.Types;

public abstract class UpdateTypeCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public UpdateTypeCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}