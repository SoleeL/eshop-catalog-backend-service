using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateBrandCommandHandler> _logger;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork, ILogger<CreateBrandCommandHandler> logger)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<BaseResponseDto<BrandResponseDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        // BUG: Si el host de la db esta caida o no alcanzable, esto devuelve informacion innecesaria al usuario

        // Crear la entidad BrandEntity
        BrandEntity brandEntity = new BrandEntity
        {
            Name = request.Name,
            Description = request.Description
        };
        
        // Agregar la nueva marca a la base de datos
        await _brandRepository.AddAsync(brandEntity);

        // Puedes realizar otras modificaciones aquí si es necesario (segundo posible cambio)
        // await _inventoryRepository.UpdateStockAsync(request.ProductId, request.Quantity);
        
        try
        {
            // Confirmar la transacción
            await _unitOfWork.CommitAsync(cancellationToken);
            
            if (brandEntity.Id == Guid.Empty) throw new EntityException("Brand could not be created. Please try again.");
        }
        catch (Exception exception)
        {
            // Si hay un error, revertir la transacción
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw exception;
        }
        
        BaseResponseDto<BrandResponseDto> baseResponseDto = new BaseResponseDto<BrandResponseDto>()
        {
            Data = CatalogMapper.Mapper.Map<BrandResponseDto>(brandEntity)
        };
        
        return baseResponseDto;
    }
}