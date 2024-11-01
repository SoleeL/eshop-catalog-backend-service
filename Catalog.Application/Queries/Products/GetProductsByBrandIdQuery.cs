using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByBrandIdQuery : IRequest<IEnumerable<ProductResponseDto>>
{
    public Guid Id { get; set; }

    public GetProductsByBrandIdQuery(Guid id)
    {
        Id = id;
    }
}