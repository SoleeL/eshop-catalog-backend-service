using Catalog.Application.DTOs;
using Catalog.Application.DTOs.Bases;
using Catalog.Application.Exceptions;
using Catalog.Application.Mappers;
using Catalog.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Brands;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BaseResponseDto<BrandResponseDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseDto<BrandResponseDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        BaseResponseDto<BrandResponseDto> baseResponseDto = new BaseResponseDto<BrandResponseDto>();
        
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        // BUG: Si el host de la db esta caida o no alcanzable, esto devuelve informacion innecesaria al usuario

        try
        {
            // Crear la entidad BrandEntity
            BrandEntity brandEntity = new BrandEntity
            {
                Name = request.Name
            };

            // Agregar la nueva marca a la base de datos
            await _brandRepository.AddAsync(brandEntity);

            // Puedes realizar otras modificaciones aquí si es necesario (segundo posible cambio)
            // await _inventoryRepository.UpdateStockAsync(request.ProductId, request.Quantity);

            // Confirmar la transacción
            await _unitOfWork.CommitAsync(cancellationToken);

            if (brandEntity.Id == Guid.Empty) throw new InvalidEntityException("Brand could not be created. Please try again.");

            // Mapear la entidad a un DTO de respuesta
            BrandResponseDto brandResponseDto = CatalogMapper.Mapper.Map<BrandResponseDto>(brandEntity);

            baseResponseDto.Data = brandResponseDto;
            return baseResponseDto;
        }
        catch (InvalidEntityException exception)
        {
            baseResponseDto.Succcess = false;
            baseResponseDto.Message = exception.Message;
            return baseResponseDto;
        }
        catch (Exception exception)
        {
            // Si hay un error, revertir la transacción
            await _unitOfWork.RollbackAsync(cancellationToken);
            baseResponseDto.Succcess = false;
            baseResponseDto.Message = exception.Message;
            return baseResponseDto;
        }
    }
}