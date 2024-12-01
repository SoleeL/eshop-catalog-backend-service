using MediatR;

namespace Catalog.Application.Commands.Categories;

public class AddCategoryCommand : IRequest<Guid>
{
    public string Name { get; set; }
    
    public AddCategoryCommand(string name)
    {
        Name = name;
    }
}