using Catalog.Application.Extensions;
using Catalog.Application.Validations.Utils;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidatorBehavior<TRequest, TResponse>> logger
    )
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        string typeName = request.GetGenericTypeName();

        _logger.LogInformation("Validating {CommandQueryType}", typeName);

        List<ValidationFailure> failures = new List<ValidationFailure>();
        
        foreach (IValidator<TRequest> validator in _validators)
        {
            if (validator is IValidatorAsync)
            {
                ValidationResult result = await validator.ValidateAsync(request, cancellationToken);
                failures.AddRange(result.Errors.Where(error => error != null));
            }
            else
            {
                failures.AddRange(validator.Validate(request).Errors.Where(error => error != null));
            }
        }
        
        if (failures.Any())
        {
            _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
                typeName,
                request,
                failures
            );
            
            throw new ValidationException("Validation exception", failures);
        }

        return await next();
    }
}