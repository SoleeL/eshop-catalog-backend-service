using MediatR;

namespace Catalog.Application.Commands.Products;

public abstract class UpdateProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public UpdateProductCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}