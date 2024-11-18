using Catalog.Application.DTOs;
using Catalog.Application.DTOs.Bases;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
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

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, BaseResponseDto<IEnumerable<BrandResponseDto>>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponseDto<IEnumerable<BrandResponseDto>>> Handle(
        GetAllBrandsQuery request,
        CancellationToken cancellationToken
    )
    {
        BaseResponseDto<IEnumerable<BrandResponseDto>> baseResponseDto = new BaseResponseDto<IEnumerable<BrandResponseDto>>();
        
        IEnumerable<BrandEntity> brandEntities = await _brandRepository.GetPageAsync(request.Page, request.Size);
        IEnumerable<BrandResponseDto> brandResponseDtos = CatalogMapper.Mapper.Map<IEnumerable<BrandResponseDto>>(brandEntities);
        
        baseResponseDto.Data = brandResponseDtos;
        return baseResponseDto;
    }
}