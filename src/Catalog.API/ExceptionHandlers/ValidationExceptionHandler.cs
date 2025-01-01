using Catalog.Application.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Catalog.API.ExceptionHandlers;

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
        CancellationToken cancellationToken
    )
    {
        if (exception is not ValidationException validationException) return false;

        _logger.LogError(exception, "ValidationException occurred: {Message}", exception.Message);

        List<ValidationErrorDto> validationErrors = validationException.Errors
            .Select(error =>
                new ValidationErrorDto
                {
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage,
                    AttemptedValue = error.AttemptedValue,
                })
            .ToList();

        BaseResponseDto<List<ValidationErrorDto>> baseResponseDto = new BaseResponseDto<List<ValidationErrorDto>>(
            false,
            validationErrors,
            "Validation error"
        );
        
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(baseResponseDto, cancellationToken);

        return true;
    }
}