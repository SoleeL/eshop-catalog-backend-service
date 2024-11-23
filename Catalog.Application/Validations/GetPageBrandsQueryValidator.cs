using Catalog.Application.Extensions;
using Catalog.Application.Queries.Brands;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Validations;

public class GetPageBrandsQueryValidator : AbstractValidator<GetPageBrandsQuery>
{
    readonly HashSet<string> _validFields = typeof(BrandEntity)
        .GetProperties()
        .SelectMany(p => new[] { p.Name, $"-{p.Name}" })
        .ToHashSet();
    
    public GetPageBrandsQueryValidator(ILogger<GetPageBrandsQueryValidator> logger)
    {
        RuleFor(query => query.Approval)
            .Must(x => x == null || Enum.IsDefined(typeof(Approval), x)) // Solo valida si Approval no es nulo.
            .When(query => query.Approval is not null)
            .WithMessage($"Approval must be a valid value ({string.Join(", ", Enum.GetNames(typeof(Approval)))}) if provided");
        
        RuleFor(query => query.Sort)
            .Must(x => x == null || x.All(field => _validFields.Contains(field)))
            .When(query => query.Sort.Any())
            .WithMessage($"Sort must be a valid value ({string.Join(", ", _validFields)}) if provided");
        
        RuleFor(query => query.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than 1");
        
        RuleFor(query => query.Size).GreaterThanOrEqualTo(1).WithMessage("Size must be greater than 1");
    }
}