using Catalog.Application.DTOs;
using Catalog.Application.DTOs.Bases;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetPageBrandsQuery : IRequest<(BaseResponseDto<IEnumerable<BrandResponseDto>>, int)>
{
    public int Page { get; set; }
    public int Size { get; set; }

    public GetPageBrandsQuery(int page, int size)
    {
        Page = page;
        Size = size;
    }
}

public class GetPageBrandsQueryHandler : IRequestHandler<GetPageBrandsQuery, (BaseResponseDto<IEnumerable<BrandResponseDto>>, int)>
{
    private readonly IBrandRepository _brandRepository;

    public GetPageBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<(BaseResponseDto<IEnumerable<BrandResponseDto>>, int)> Handle(
        GetPageBrandsQuery request,
        CancellationToken cancellationToken
    )
    {
        BaseResponseDto<IEnumerable<BrandResponseDto>> baseResponseDto = new BaseResponseDto<IEnumerable<BrandResponseDto>>();

        (IEnumerable<BrandEntity> brandEntities, int totalCount) = await _brandRepository.GetPageAsync(request.Page, request.Size);
        IEnumerable<BrandResponseDto> brandResponseDtos = CatalogMapper.Mapper.Map<IEnumerable<BrandResponseDto>>(brandEntities);
        
        baseResponseDto.Data = brandResponseDtos;
        
        return (baseResponseDto, totalCount);
    }
}