using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Application.Validations.Utils;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Commands.BrandStates;

public class CreateBrandStateCommand : IRequest<BaseResponseDto<BrandStateDto>>
{
    public string Name { get; }
    public string? Description { get; }
}

public class CreateBrandStateCommandValidator : AbstractValidator<CreateBrandStateCommand>, IValidatorAsync
{
    public CreateBrandStateCommandValidator(IBrandStateRepository brandStateRepository)
    {
        RuleFor(createBrand => createBrand.Name)
            .Cascade(CascadeMode.Stop) // Detener al primer error y evitar multiples mensajes de validacion
            .NotNull()
            .WithMessage("Brand state name cannot be null")
            .NotEmpty()
            .WithMessage("Brand name state cannot be empty or only spaces")
            .MaximumLength(50)
            .WithMessage("Brand name state must not exceed 50 characters")
            .MustAsync(async (name, cancellation) => !await brandStateRepository.NameExists(name.ToLower()))
            .WithMessage("Brand state name already used");
        
        RuleFor(createBrand => createBrand.Description)
            .Cascade(CascadeMode.Stop) // Detener al primer error y evitar multiples mensajes de validacion 
            .NotNull()
            .WithMessage("Brand description cannot be null")
            .NotEmpty()
            .WithMessage("Brand description cannot be empty")
            .MaximumLength(255)
            .WithMessage("Brand description must not exceed 255 characters");
    }
}

public class CreateBrandStateCommandHandler : IRequestHandler<CreateBrandStateCommand, BaseResponseDto<BrandStateDto>>
{
    private readonly IBrandStateRepository _brandStateRepository;
    private readonly ILogger<CreateBrandStateCommandHandler> _logger;

    public CreateBrandStateCommandHandler(
        IBrandStateRepository brandStateRepository,
        ILogger<CreateBrandStateCommandHandler> logger
    )
    {
        _brandStateRepository = brandStateRepository;
        _logger = logger;
    }

    public async Task<BaseResponseDto<BrandStateDto>> Handle(
        CreateBrandStateCommand request,
        CancellationToken cancellationToken
    )
    {
        // Crear la entidad BrandEntity
        BrandStateEntity brandStateEntity = CatalogMapper.Mapper.Map<BrandStateEntity>(request);
        
        // Agregar la nueva marca a la base de datos
        await _brandStateRepository.AddWithSaveChange(brandStateEntity, cancellationToken);

        // TODO: ESTO ES POSIBLE QUE OCURRA?
        // if (brandStateEntity.Id == 0) throw new EntityException("Brand not be created. Please try again.");

        BaseResponseDto<BrandStateDto> baseResponseDto = new BaseResponseDto<BrandStateDto>(
            CatalogMapper.Mapper.Map<BrandStateDto>(brandStateEntity));

        return baseResponseDto;
    }
}