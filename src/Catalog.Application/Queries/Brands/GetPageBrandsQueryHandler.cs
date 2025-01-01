using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Application.Validations.Utils;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetPageBrandsQuery : BaseQuery<BaseResponseDto<IEnumerable<BrandDto>>>
{
    public bool? Enabled { get; } // Filtration
    public int? StateId { get; set; } // Filtration // Necesita valicacion asincrona
    public string? Search { get; } // Search

    public List<string> Sort { get; } // Sorting -> ASC = "id", DESC = "-id"

    public int Page { get; } = 1; // Pagination
    public int Size { get; } = 10; // Pagination

    public GetPageBrandsQuery(
        bool? enabled,
        int? stateId,
        string? search,
        string? sort,
        int? page,
        int? size
    )
    {
        Enabled = enabled;
        StateId = stateId;
        Search = search;
        Sort = ParseToList(sort);
        Page = page ?? Page;
        Size = size ?? Size;
    }
}

public class GetPageBrandsQueryValidator : AbstractValidator<GetPageBrandsQuery>, IValidatorAsync
{
    readonly HashSet<string> _validFields = ValidFieldsUtil.GetValidFields<BrandEntity>();
    
    public GetPageBrandsQueryValidator(IBrandStateRepository brandStateRepository)
    {
        RuleFor(getPageBrandsQuery => getPageBrandsQuery.StateId)
            .MustAsync(async (stateId, cancellation) => stateId == null || await brandStateRepository.StateExists(stateId.Value))
            .WithMessage("StateId must be a valid value");
        
        RuleFor(query => query.Sort)
            .Must(x => x == null || x.All(field => _validFields.Contains(field)))
            .WithMessage($"Sort must be a valid value ({string.Join(", ", _validFields)}) if provided")
            .When(query => query.Sort.Any());
        
        RuleFor(query => query.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than 1");
        
        RuleFor(query => query.Size).GreaterThanOrEqualTo(1).WithMessage("Size must be greater than 1");
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
            request.StateId,
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