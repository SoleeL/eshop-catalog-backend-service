using MediatR;

namespace Catalog.Application.Commands.Categories;

public class UpdateCategoryCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public UpdateCategoryCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}