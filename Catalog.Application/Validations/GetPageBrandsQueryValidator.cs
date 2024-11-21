using Catalog.Application.Queries.Brands;
using Catalog.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Validations;

public class GetPageBrandsQueryValidator : AbstractValidator<GetPageBrandsQuery>
{
    public GetPageBrandsQueryValidator(ILogger<GetPageBrandsQueryValidator> logger)
    {
        RuleFor(query => query.Enabled);
        
        // RuleFor(query => query.Approval)
        //     .Must(a => a == null || Enum.IsDefined(typeof(Approval), a))
        //     .WithMessage("Approval must be a valid value (Pending, Approved, Rejected) if provided");
        //
        
        RuleFor(query => query.Approval)
            .Must(x => x == null || Enum.IsDefined(typeof(Approval), x)) // Solo valida si Approval no es nulo.
            .When(query => query.Approval is not null)
            .WithMessage("Approval must be a valid value (Pending, Approved, Rejected) if provided");
        
        RuleFor(query => query.Search);
        
        RuleFor(query => query.Sort);
        
        RuleFor(query => query.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than 1");
        
        RuleFor(query => query.Size).GreaterThanOrEqualTo(1).WithMessage("Size must be greater than 1");
    }
}