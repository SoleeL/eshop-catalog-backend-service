using System.Data;
using System.Data.Common;
using Catalog.Application.Dtos;
using Catalog.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.ExceptionHandlers;

public class DbTransactionExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DbTransactionExceptionHandler> _logger;

    public DbTransactionExceptionHandler(ILogger<DbTransactionExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not (DbException or DbUpdateException or EntityException or DataException
            or ConstraintException or EntityException)) return false;

        _logger.LogError(exception, "Exception type: {Exception}", exception.GetType().Name);
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        if (exception.InnerException is not null)
        {
            _logger.LogError(exception.InnerException, "InnerException type: {Exception}",
                exception.InnerException.GetType().Name);
            _logger.LogError(exception.InnerException, "InnerException occurred: {Message}",
                exception.InnerException.Message);
        }

        BaseResponseDto<ProblemDetails> baseResponseDto = new BaseResponseDto<ProblemDetails>(
            false,
            new ProblemDetails
            {
                Type = exception.InnerException?.GetType().Name ?? exception.GetType().Name,
                Title = exception.Message,
                Detail = exception.InnerException?.Message ?? null
            },
            "Database transaction error"
        );

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(baseResponseDto, cancellationToken);

        return true;
    }
}