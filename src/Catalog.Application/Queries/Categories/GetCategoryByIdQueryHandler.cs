using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Application.Queries.Brands;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Categories;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDto?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponseDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        CategoryEntity? categoryEntity = await _categoryRepository.GetByIdAsync(request.Id);

        if (categoryEntity == null) return null;
        CategoryResponseDto categoryResponseDto = CatalogMapper.Mapper.Map<CategoryResponseDto>(categoryEntity);
        return categoryResponseDto;
    }
}