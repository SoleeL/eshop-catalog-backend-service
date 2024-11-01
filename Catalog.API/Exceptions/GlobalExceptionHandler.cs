using Catalog.Application.DTOs.Bases;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Exceptions;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    // README: Para estudiar mas:
    // https://www.milanjovanovic.tech/blog/global-error-handling-in-aspnetcore-8
    // https://medium.com/@AntonAntonov88/handling-errors-with-iexceptionhandler-in-asp-net-core-8-0-48c71654cc2e
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        BaseResponseDto<ProblemDetails> baseResponseDto = new BaseResponseDto<ProblemDetails>();
        
        baseResponseDto.Succcess = false;
        baseResponseDto.Data = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = exception.Message
        };
        baseResponseDto.Message = "Server error";
        
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(baseResponseDto, cancellationToken);
        
        return true;
    }
}