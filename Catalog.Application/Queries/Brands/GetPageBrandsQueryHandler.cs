using Catalog.Application.Dtos;
using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetPageBrandsQuery : BaseQuery<BaseResponseDto<IEnumerable<BrandResponseDto>>>
{
    public bool? Enabled { get; init; } // Filtration
    public string? Approval { get; init; } // Filtration

    public string? Search { get; init; } // Search

    public List<string> Sort { get; init; } // Sorting -> ASC = "id", DESC = "-id"

    public int Page { get; init; } = 1; // Pagination
    public int Size { get; init; } = 10; // Pagination

    public GetPageBrandsQuery(
        bool? enabled = null,
        string? approval = null,
        string? search = null,
        string? sort = null,
        int? page = null,
        int? size = null
    )
    {
        Enabled = enabled;
        Approval = approval;
        Search = search;
        Sort = ParseToList(sort);
        Page = page ?? Page;
        Size = size ?? Size;
    }
}

public class GetPageBrandsQueryHandler : IRequestHandler<GetPageBrandsQuery, BaseResponseDto<IEnumerable<BrandResponseDto>>>
{
    private readonly IBrandRepository _brandRepository;

    public GetPageBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponseDto<IEnumerable<BrandResponseDto>>> Handle(
        GetPageBrandsQuery request,
        CancellationToken cancellationToken
    )
    {
        BaseResponseDto<IEnumerable<BrandResponseDto>> baseResponseDto = new BaseResponseDto<IEnumerable<BrandResponseDto>>();

        (IEnumerable<BrandEntity> brandEntities, int totalItemCount) = await _brandRepository.GetPageAsync(
            request.Enabled,
            Enum.TryParse(request.Approval, out Approval approvalParsed) ? approvalParsed : null,
            request.Search,
            request.Sort,
            request.Page,
            request.Size
        );

        IEnumerable<BrandResponseDto> brandResponseDtos = CatalogMapper.Mapper.Map<IEnumerable<BrandResponseDto>>(brandEntities);

        baseResponseDto.Data = brandResponseDtos;
        baseResponseDto.TotalItemCount = totalItemCount;
        
        return baseResponseDto;
    }
}