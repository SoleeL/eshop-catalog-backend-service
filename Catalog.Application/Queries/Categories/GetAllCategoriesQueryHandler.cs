using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Categories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponseDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryResponseDto>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<CategoryEntity> categoryEntities = await _categoryRepository.GetAllAsync();
        IEnumerable<CategoryResponseDto> categoryResponseDtos = CatalogMapper.Mapper.Map<IEnumerable<CategoryResponseDto>>(categoryEntities);
        return categoryResponseDtos;
    }
}