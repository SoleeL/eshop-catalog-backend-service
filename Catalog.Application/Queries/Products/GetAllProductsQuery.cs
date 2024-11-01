using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Products;

public abstract class GetAllProductsQuery : IRequest<IEnumerable<ProductResponseDto>>
{
}