using Catalog.Application.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Catalog.API.ExceptionHandlers;

public class DbConnectionExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DbConnectionExceptionHandler> _logger;
    
    public DbConnectionExceptionHandler(ILogger<DbConnectionExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not (NpgsqlException or TimeoutException or InvalidOperationException )) return false;
        
        _logger.LogError(exception, "Exception type: {Exception}", exception.GetType().Name);
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        if (exception.InnerException is not null)
        {
            _logger.LogError(exception.InnerException, "InnerException type: {Exception}", exception.InnerException.GetType().Name);
            _logger.LogError(exception.InnerException, "InnerException occurred: {Message}", exception.InnerException.Message);
        }
        
        BaseResponseDto<ProblemDetails> baseResponseDto = new BaseResponseDto<ProblemDetails>()
        {
            Succcess = false,
            Data = new ProblemDetails
            {
                Type = exception.InnerException?.GetType().Name ?? exception.GetType().Name,
                // Title = exception.Message,
                Detail = exception.InnerException?.Message ?? exception.Message
            },
            Message = "Database connection error"
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(baseResponseDto, cancellationToken);
        
        return true;
    }
}