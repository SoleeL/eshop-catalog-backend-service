using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Commands.Brands;

public class CreateBrandCommandHandlerMultiRepository : IRequestHandler<CreateBrandCommand, BaseResponseDto<BrandResponseDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateBrandCommandHandler> _logger;

    public CreateBrandCommandHandlerMultiRepository(IBrandRepository brandRepository, IUnitOfWork unitOfWork, ILogger<CreateBrandCommandHandler> logger)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<BaseResponseDto<BrandResponseDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        // Crear la entidad BrandEntity -> TODO: Mapper de CreateBrandCommand a BrandEntity??
        BrandEntity brandEntity = new BrandEntity
        {
            Name = request.Name,
            Description = request.Description
        };
        
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Agregar la nueva marca a la base de datos
            await _brandRepository.AddAsync(brandEntity);

            // Puedes realizar otras modificaciones aquí si es necesario (segundo posible cambio)
            // DESCOMENTAR
            // await _inventoryRepository.UpdateStockAsync(request.ProductId, request.Quantity);
            
            // Confirmar la transacción
            await _unitOfWork.CommitAsync(cancellationToken);
            
            if (brandEntity.Id == Guid.Empty) throw new EntityException("Brand not be created. Please try again.");
        }
        catch (Exception exception)
        {
            // La OperationCanceledException no es tratada por defecto como una excepción grave o no controlada, por lo que
            // puede no llegar al manejador global de excepciones. Deberás capturarla explícitamente en tu código.
            
            // Si hay un error, revertir la transacción
            await _unitOfWork.RollbackAsync();
            throw;
        }

        BaseResponseDto<BrandResponseDto> baseResponseDto = new BaseResponseDto<BrandResponseDto>(CatalogMapper.Mapper.Map<BrandResponseDto>(brandEntity));
        
        return baseResponseDto;
    }
}