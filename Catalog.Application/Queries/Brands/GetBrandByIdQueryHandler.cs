using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetBrandByIdQuery : IRequest<BaseResponseDto<BrandResponseDto>>
{
    public Guid Guid { get; set; }

    public GetBrandByIdQuery(Guid guid)
    {
        Guid = guid;
    }
}

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BaseResponseDto<BrandResponseDto>>
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandByIdQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponseDto<BrandResponseDto>> Handle(
        GetBrandByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        // 
        
        BrandEntity? brandEntity = await _brandRepository.GetByIdAsync(request.Guid, cancellationToken);
        
        BrandResponseDto brandResponseDto = CatalogMapper.Mapper.Map<BrandResponseDto>(brandEntity);
        
        BaseResponseDto<BrandResponseDto> baseResponseDto = new BaseResponseDto<BrandResponseDto>(brandResponseDto);
        
        if (brandEntity == null)
        {
            baseResponseDto.Succcess = false;
            baseResponseDto.Message = "Brand does not exist";
        }
        
        return baseResponseDto;
    }
}