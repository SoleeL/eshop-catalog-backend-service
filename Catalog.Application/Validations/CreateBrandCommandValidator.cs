using Catalog.Application.Commands.Brands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Validations;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator(ILogger<CreateBrandCommandValidator> logger)
    {
        RuleFor(createBrand => createBrand.Name)
            .Cascade(CascadeMode.Stop) // Detener al primer error y evitar multiples mensajes de validacion
            .NotNull()
            .WithMessage("Brand name cannot be null.")
            .NotEmpty()
            .WithMessage("Brand name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Brand name must not exceed 100 characters.");
        
        RuleFor(createBrand => createBrand.Description)
            .Cascade(CascadeMode.Stop) // Detener al primer error y evitar multiples mensajes de validacion 
            .NotNull()
            .WithMessage("Brand description cannot be null.")
            .NotEmpty()
            .WithMessage("Brand description cannot be empty.")
            .MaximumLength(255)
            .WithMessage("Brand description must not exceed 255 characters.");
    }
}