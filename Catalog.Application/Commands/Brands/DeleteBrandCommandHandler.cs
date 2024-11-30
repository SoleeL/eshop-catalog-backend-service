using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Brands;

public class DeleteBrandCommand : IRequest<BaseResponseDto<BrandResponseDto>>
{
    public Guid Guid { get; set; }

    public DeleteBrandCommand(Guid guid)
    {
        Guid = guid;
    }
}

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, BaseResponseDto<BrandResponseDto>>
{
    private readonly IBrandRepository _brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponseDto<BrandResponseDto>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        BrandEntity? brandEntity = await _brandRepository.DeleteWithSaveChange(request.Guid, cancellationToken);
        
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