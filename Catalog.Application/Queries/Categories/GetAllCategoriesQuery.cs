using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Categories;

public abstract class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResponseDto>>
{
    
}