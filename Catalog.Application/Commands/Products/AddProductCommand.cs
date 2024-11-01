using MediatR;

namespace Catalog.Application.Commands.Products;

public abstract class AddProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
    
    public AddProductCommand(string name)
    {
        Name = name;
    }
}