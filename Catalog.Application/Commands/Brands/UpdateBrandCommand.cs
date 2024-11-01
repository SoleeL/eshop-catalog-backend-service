using MediatR;

namespace Catalog.Application.Commands.Brands;

public class UpdateBrandCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public UpdateBrandCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}