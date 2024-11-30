using Catalog.Application.Commands.Brands;
using Catalog.Application.Validations.Utils;
using Catalog.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Validations;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>, IValidatorAsync
{
    public CreateBrandCommandValidator(IBrandRepository brandRepository, ILogger<CreateBrandCommandValidator> logger)
    {
        RuleFor(createBrand => createBrand.Name)
            .Cascade(CascadeMode.Stop) // Detener al primer error y evitar multiples mensajes de validacion
            .NotNull()
            .WithMessage("Brand name cannot be null")
            .NotEmpty()
            .WithMessage("Brand name cannot be empty or only spaces")
            .MaximumLength(100)
            .WithMessage("Brand name must not exceed 100 characters")
            .MustAsync(async (name, cancellation) => !await brandRepository.BrandNameExistsAsync(name.ToLower()))
            .WithMessage("Brand name already used");
        
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