using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Application.Validations.Utils;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Commands.Brands;

public class UpdateBrandCommand : IRequest<BaseResponseDto<BrandDto>>
{
    public Guid Guid { get; set; } = Guid.Empty;

    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Enabled { get; set; }
    public int? State { get; set; }
}

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>, IValidatorAsync
{
    public UpdateBrandCommandValidator(IBrandRepository brandRepository, IBrandStateRepository brandStateRepository)
    {
        RuleFor(updateBrandCommand => updateBrandCommand.Name)
            .Cascade(CascadeMode.Stop) // Detener al primer error y evitar multiples mensajes de validacion
            .Must(name => name == null || name != string.Empty)
            .WithMessage("Brand name cannot be empty")
            .Must(name => name == null || name.Trim() != string.Empty)
            .WithMessage("Brand name cannot be only spaces")
            .MaximumLength(100)
            .WithMessage("Brand name must not exceed 100 characters")
            .MustAsync(async (name, cancellation) => !await brandRepository.NameExists(name.ToLower()))
            .WithMessage("Brand name already used");
        
        RuleFor(updateBrandCommand => updateBrandCommand.Description)
            .Cascade(CascadeMode.Stop)
            .Must(description => description == null || description != string.Empty)
            .WithMessage("Brand description cannot be empty")
            .Must(description => description == null || description.Trim() != string.Empty)
            .WithMessage("Brand description cannot be only spaces")
            .MaximumLength(255)
            .WithMessage("Brand description must not exceed 255 characters");
        
        RuleFor(updateBrandCommand => updateBrandCommand.State)
            // README stateId.Value: Accede al valor cuando no es null, asegurando la compatibilidad con el método.
            .MustAsync(async (stateId, cancellation) => stateId == null || !await brandStateRepository.StateExists(stateId.Value))
            .WithMessage("State must be a valid value");
    }
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
            request.State,
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