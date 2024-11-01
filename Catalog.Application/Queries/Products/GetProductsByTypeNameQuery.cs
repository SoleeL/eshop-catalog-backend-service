using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByTypeNameQuery : IRequest<IEnumerable<ProductResponseDto>>
{
    public string Name { get; set; }

    public GetProductsByTypeNameQuery(string name)
    {
        Name = name;
    }
}