using Catalog.Application.DTOs;
using Catalog.Application.DTOs.Bases;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetAllBrandsQuery : IRequest<BaseResponseDto<IEnumerable<BrandResponseDto>>>
{
    public int Page { get; set; }
    public int Size { get; set; }

    public GetAllBrandsQuery(int page, int size)
    {
        Page = page;
        Size = size;
    }
}