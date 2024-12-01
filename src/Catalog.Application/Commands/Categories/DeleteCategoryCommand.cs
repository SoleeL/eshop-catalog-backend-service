using MediatR;

namespace Catalog.Application.Commands.Categories;

public class DeleteCategoryCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
}