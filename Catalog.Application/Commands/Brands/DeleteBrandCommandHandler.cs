using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Domain.Shared;
using MediatR;

namespace Catalog.Application.Commands.Brands;

public class DeleteBrandCommand : IRequest<BaseResponseDto<BrandDto>>
{
    public Guid Guid { get; set; }

    public DeleteBrandCommand(Guid guid)
    {
        Guid = guid;
    }
}

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, BaseResponseDto<BrandDto>>
{
    private readonly IBrandRepository _brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponseDto<BrandDto>> Handle(
        DeleteBrandCommand request,
        CancellationToken cancellationToken
    )
    {
        BrandEntity? brandEntity = await _brandRepository.DeleteWithSaveChange(request.Guid, cancellationToken);

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

public class
    DeleteBrandCommandHandlerMultiRepository : IRequestHandler<DeleteBrandCommand, BaseResponseDto<BrandDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBrandCommandHandlerMultiRepository(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseDto<BrandDto>> Handle(DeleteBrandCommand request,
        CancellationToken cancellationToken)
    {
        BaseResponseDto<BrandDto> baseResponseDto;
        BrandEntity? brandEntity;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Iniciar la eliminacion de la marca en el servicio, aun no en la base de datos
            brandEntity = await _brandRepository.DeleteAsync(request.Guid);

            baseResponseDto = new BaseResponseDto<BrandDto>(
                CatalogMapper.Mapper.Map<BrandDto>(brandEntity));

            // Confirmar la transacción
            if (brandEntity == null)
            {
                baseResponseDto.Succcess = false;
                baseResponseDto.Message = "Brand does not exist";
            }
            else
            {
                // Puedes realizar otras modificaciones aquí si es necesario (segundo posible cambio)
                // DESCOMENTAR
                // await _inventoryRepository.UpdateStockAsync(request.ProductId, request.Quantity);
                await _unitOfWork.CommitAsync(cancellationToken);
            }
        }
        catch (Exception exception)
        {
            // La OperationCanceledException no es tratada por defecto como una excepción grave o no controlada, por lo que
            // puede no llegar al manejador global de excepciones. Deberás capturarla explícitamente en tu código.

            // Si hay un error, revertir la transacción
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return baseResponseDto;
    }
}