using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Products;

public abstract class GetProductByBrandNameQuery : IRequest<IEnumerable<ProductResponseDto>>
{
    public string Name { get; set; }

    public GetProductByBrandNameQuery(string name)
    {
        Name = name;
    }
}