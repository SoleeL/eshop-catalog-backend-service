using Catalog.Application.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Catalog.API.ExceptionHandlers;

public class ValidationError
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
    public object AttemptedValue { get; set; }

    public ValidationError(string propertyName, string errorMessage, object attemptedValue)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
        AttemptedValue = attemptedValue;
    }
}

internal sealed class ValidationExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ValidationExceptionHandler> _logger;

    public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException) return false;

        _logger.LogError(exception, "ValidationException occurred: {Message}", exception.Message);

        List<ValidationError> validationErrors = validationException.Errors.Select(error =>
            new ValidationError(error.PropertyName, error.ErrorMessage, error.AttemptedValue)).ToList();
        
        BaseResponseDto<List<ValidationError>> baseResponseDto = new BaseResponseDto<List<ValidationError>>
        {
            Succcess = false,
            Data = validationErrors,
            Message = "Validation error"
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(baseResponseDto, cancellationToken);

        return true;
    }
}