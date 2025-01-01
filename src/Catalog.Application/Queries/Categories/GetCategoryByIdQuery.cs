using Catalog.Application.Dtos.Entities;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Queries.Categories;

public abstract class GetCategoryByIdQuery : IRequest<CategoryResponseDto>
{
    public Guid Id { get; set; }

    public GetCategoryByIdQuery(Guid id)
    {
        Id = id;
    }
}