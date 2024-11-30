using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetBrandByIdQuery : IRequest<BaseResponseDto<BrandDto>>
{
    public Guid Guid { get; set; }

    public GetBrandByIdQuery(Guid guid)
    {
        Guid = guid;
    }
}

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BaseResponseDto<BrandDto>>
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandByIdQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponseDto<BrandDto>> Handle(
        GetBrandByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        // 
        
        BrandEntity? brandEntity = await _brandRepository.GetByIdAsync(request.Guid, cancellationToken);
        
        BrandDto brandDto = CatalogMapper.Mapper.Map<BrandDto>(brandEntity);
        
        BaseResponseDto<BrandDto> baseDto = new BaseResponseDto<BrandDto>(brandDto);
        
        if (brandEntity == null)
        {
            baseDto.Succcess = false;
            baseDto.Message = "Brand does not exist";
        }
        
        return baseDto;
    }
}