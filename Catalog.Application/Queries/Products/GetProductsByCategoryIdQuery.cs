using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByCategoryIdQuery : IRequest<IEnumerable<ProductResponseDto>>
{
    public Guid Id { get; set; }

    public GetProductsByCategoryIdQuery(Guid id)
    {
        Id = id;
    }
}