using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Products;

public abstract class GetProductsByNameQuery : IRequest<IEnumerable<ProductResponseDto>>
{
    public string Name { get; set; }

    public GetProductsByNameQuery(string name)
    {
        Name = name;
    }
}