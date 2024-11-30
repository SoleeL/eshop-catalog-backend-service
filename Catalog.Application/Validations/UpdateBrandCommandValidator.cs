using Catalog.Application.Commands.Brands;
using Catalog.Domain.Enums;
using FluentValidation;

namespace Catalog.Application.Validations;

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(updateBrandCommand => updateBrandCommand.Name)
            .Cascade(CascadeMode.Stop) // Detener al primer error y evitar multiples mensajes de validacion
            // .NotEmpty()
            .Must(name => name == null || name != string.Empty)
            .WithMessage("Brand name cannot be empty")
            .Must(name => name == null || name.Trim() != string.Empty)
            .WithMessage("Brand name cannot be only spaces")
            .MaximumLength(100)
            .WithMessage("Brand name must not exceed 100 characters");
            // .When(name => name != null)
        
        RuleFor(updateBrandCommand => updateBrandCommand.Description)
            .Cascade(CascadeMode.Stop)
            .Must(description => description == null || description != string.Empty)
            .WithMessage("Brand description cannot be empty")
            .Must(description => description == null || description.Trim() != string.Empty)
            .WithMessage("Brand description cannot be only spaces")
            .MaximumLength(255)
            .WithMessage("Brand description must not exceed 255 characters");
            // .When(description => description != null)
        
        RuleFor(updateBrandCommand => updateBrandCommand.Approval)
            .Must(approval => approval == null || Enum.IsDefined(typeof(Approval), approval))
            .WithMessage($"Approval must be a valid value ({string.Join(", ", Enum.GetNames(typeof(Approval)))})");
    }
}