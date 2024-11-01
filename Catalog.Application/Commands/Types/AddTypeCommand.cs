using MediatR;

namespace Catalog.Application.Commands.Types;

public abstract class AddTypeCommand : IRequest<Guid>
{
    public string Name { get; set; }
    
    public AddTypeCommand(string name)
    {
        Name = name;
    }
}