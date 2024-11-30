using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Brands;

public class UpdateBrandCommand : IRequest<BaseResponseDto<BrandDto>>
{
    public Guid Guid { get; set; } = Guid.Empty;

    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Enabled { get; set; }
    public string? Approval { get; set; }
}

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BaseResponseDto<BrandDto>>
{
    private readonly IBrandRepository _brandRepository;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponseDto<BrandDto>> Handle(
        UpdateBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        BrandEntity? brandEntity = await _brandRepository.UpdateWithSaveChange(
            request.Guid,
            request.Name,
            request.Description,
            request.Enabled,
            Enum.TryParse(request.Approval, out Approval approvalParsed) ? approvalParsed : null,
            cancellationToken);

        BrandDto brandDto = CatalogMapper.Mapper.Map<BrandDto>(brandEntity);

        BaseResponseDto<BrandDto> baseResponseDto = new BaseResponseDto<BrandDto>(brandDto);

        if (brandEntity == null)
        {
            baseResponseDto.Succcess = false;
            baseResponseDto.Message = "Brand does not exist";
        }

        return baseResponseDto;
    }
}