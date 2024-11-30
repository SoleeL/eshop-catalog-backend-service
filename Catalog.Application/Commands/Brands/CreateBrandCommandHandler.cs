using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Commands.Brands;

public class CreateBrandCommand : IRequest<BaseResponseDto<BrandResponseDto>>
{
    public string Name { get; }
    public string Description { get; }

    public CreateBrandCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BaseResponseDto<BrandResponseDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly ILogger<CreateBrandCommandHandler> _logger;

    public CreateBrandCommandHandler(
        IBrandRepository brandRepository,
        ILogger<CreateBrandCommandHandler> logger
    )
    {
        _brandRepository = brandRepository;
        _logger = logger;
    }

    public async Task<BaseResponseDto<BrandResponseDto>> Handle(
        CreateBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        // Crear la entidad BrandEntity
        BrandEntity brandEntity = CatalogMapper.Mapper.Map<BrandEntity>(request);
        
        // Agregar la nueva marca a la base de datos
        await _brandRepository.AddWithSaveChange(brandEntity, cancellationToken);

        if (brandEntity.Id == Guid.Empty) throw new EntityException("Brand not be created. Please try again.");

        BaseResponseDto<BrandResponseDto> baseResponseDto = new BaseResponseDto<BrandResponseDto>(
            CatalogMapper.Mapper.Map<BrandResponseDto>(brandEntity));

        return baseResponseDto;
    }
}