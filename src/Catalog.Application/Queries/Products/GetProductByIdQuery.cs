using Catalog.Application.Dtos.Entities;
using MediatR;

namespace Catalog.Application.Queries.Products;

public abstract class GetProductByIdQuery : IRequest<ProductResponseDto>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}