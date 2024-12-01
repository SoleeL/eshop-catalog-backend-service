using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Categories;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        CategoryEntity? categoryEntity = await _categoryRepository.GetByIdAsync(request.Id);

        if (categoryEntity == null) return false;
        
        categoryEntity.Name = request.Name;

        await _categoryRepository.UpdateAsync(categoryEntity);
        return true;
    }
}