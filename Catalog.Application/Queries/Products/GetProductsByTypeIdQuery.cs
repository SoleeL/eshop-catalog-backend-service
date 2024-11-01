using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByTypeIdQuery : IRequest<IEnumerable<ProductResponseDto>>
{
    public Guid Id { get; set; }

    public GetProductsByTypeIdQuery(Guid id)
    {
        Id = id;
    }
}