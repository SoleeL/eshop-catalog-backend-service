using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Application.Validations.Utils;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Commands.BrandStates;

public class UpdateBrandStateCommand : IRequest<BaseResponseDto<BrandStateDto>>
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class UpdateBrandStateCommandValidator : AbstractValidator<UpdateBrandStateCommand>, IValidatorAsync
{
    public UpdateBrandStateCommandValidator(IBrandStateRepository brandStateRepository)
    {
        RuleFor(updateBrandStateCommand => updateBrandStateCommand.Name)
            .Cascade(CascadeMode.Stop) // Detener al primer error y evitar multiples mensajes de validacion
            .Must(name => name == null || name != string.Empty)
            .WithMessage("Brand state name cannot be empty")
            .Must(name => name == null || name.Trim() != string.Empty)
            .WithMessage("Brand state name cannot be only spaces")
            .MaximumLength(50)
            .WithMessage("Brand state name must not exceed 50 characters")
            .MustAsync(async (name, cancellation) => !await brandStateRepository.NameExists(name.ToLower()))
            .WithMessage("Brand state name already used");
        
        RuleFor(updateBrandStateCommand => updateBrandStateCommand.Description)
            .Cascade(CascadeMode.Stop)
            .Must(description => description == null || description != string.Empty)
            .WithMessage("Brand state description cannot be empty")
            .Must(description => description == null || description.Trim() != string.Empty)
            .WithMessage("Brand state description cannot be only spaces")
            .MaximumLength(255)
            .WithMessage("Brand state description must not exceed 255 characters");
    }
}

public class UpdateBrandStateCommandHandler : IRequestHandler<UpdateBrandStateCommand, BaseResponseDto<BrandStateDto>>
{
    private readonly IBrandStateRepository _brandStateRepository;

    public UpdateBrandStateCommandHandler(IBrandStateRepository brandStateRepository)
    {
        _brandStateRepository = brandStateRepository;
    }
    
    public async Task<BaseResponseDto<BrandStateDto>> Handle(UpdateBrandStateCommand request, CancellationToken cancellationToken)
    {
        BrandStateEntity? brandStateEntity = await _brandStateRepository.UpdateWithSaveChange(
            request.Id, 
            request.Name, 
            request.Description);
        
        BrandStateDto brandStateDto = CatalogMapper.Mapper.Map<BrandStateDto>(brandStateEntity);
        
        BaseResponseDto<BrandStateDto> baseResponseDto = new BaseResponseDto<BrandStateDto>(brandStateDto);

        if (brandStateEntity == null)
        {
            baseResponseDto.Succcess = false;
            baseResponseDto.Message = "Brand state does not exist";
        }
        
        return baseResponseDto;
    }
}