using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public abstract class GetAllBrandsQuery : IRequest<IEnumerable<BrandResponseDto>>
{
}