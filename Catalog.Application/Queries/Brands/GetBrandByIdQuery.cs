using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public abstract class GetBrandByIdQuery : IRequest<BrandResponseDto>
{
    public Guid Id { get; set; }

    public GetBrandByIdQuery(Guid id)
    {
        Id = id;
    }
}