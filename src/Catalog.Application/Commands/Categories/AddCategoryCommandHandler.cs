using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Categories;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;

    public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        CategoryEntity brandEntity = new CategoryEntity { Name = request.Name };

        await _categoryRepository.AddAsync(brandEntity);

        return brandEntity.Id;
    }
}