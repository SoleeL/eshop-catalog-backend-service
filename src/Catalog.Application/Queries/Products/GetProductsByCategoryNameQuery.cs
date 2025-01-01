using Catalog.Application.Dtos.Entities;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductByCategoryNameQuery : IRequest<IEnumerable<ProductResponseDto>>
{
    public string Name { get; set; }

    public GetProductByCategoryNameQuery(string name)
    {
        Name = name;
    }
}