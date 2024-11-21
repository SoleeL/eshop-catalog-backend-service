using Catalog.Application.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Exceptions;

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
        
        BaseResponseDto<List<ValidationFailure>> baseResponseDto = new BaseResponseDto<List<ValidationFailure>>
        {
            Succcess = false,
            Data = validationException.Errors as List<ValidationFailure>,
            Message = "Validation error"
        };
        
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(baseResponseDto, cancellationToken);

        return true;
    }
}