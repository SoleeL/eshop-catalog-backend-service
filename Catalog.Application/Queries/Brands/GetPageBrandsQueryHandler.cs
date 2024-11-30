using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetPageBrandsQuery : BaseQuery<BaseResponseDto<IEnumerable<BrandDto>>>
{
    public bool? Enabled { get; } // Filtration
    public string? Approval { get; } // Filtration // Necesita valicacion

    public string? Search { get; } // Search

    public List<string> Sort { get; } // Sorting -> ASC = "id", DESC = "-id"

    public int Page { get; } = 1; // Pagination
    public int Size { get; } = 10; // Pagination

    public GetPageBrandsQuery(
        bool? enabled,
        string? approval,
        string? search,
        string? sort,
        int? page,
        int? size
    )
    {
        Enabled = enabled;
        Approval = Capitalize(approval);
        Search = search;
        Sort = ParseToList(sort);
        Page = page ?? Page;
        Size = size ?? Size;
    }
}

public class GetPageBrandsQueryHandler : IRequestHandler<GetPageBrandsQuery, BaseResponseDto<IEnumerable<BrandDto>>>
{
    private readonly IBrandRepository _brandRepository;

    public GetPageBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponseDto<IEnumerable<BrandDto>>> Handle(
        GetPageBrandsQuery request,
        CancellationToken cancellationToken
    )
    {
        (IEnumerable<BrandEntity> brandEntities, int totalItemCount) = await _brandRepository.GetPageAsync(
            cancellationToken,
            request.Enabled,
            Enum.TryParse(request.Approval, out Approval approvalParsed) ? approvalParsed : null,
            request.Search,
            request.Sort,
            request.Page,
            request.Size
        );

        IEnumerable<BrandDto> brandDtos = CatalogMapper.Mapper.Map<IEnumerable<BrandDto>>(brandEntities);

        BaseResponseDto<IEnumerable<BrandDto>> baseResponseDtos = new BaseResponseDto<IEnumerable<BrandDto>>(
            brandDtos,
            totalItemCount
        );

        return baseResponseDtos;
    }
}