using System.Text;
using Catalog.Application.Dtos;
using Microsoft.AspNetCore.Diagnostics;

namespace Catalog.API.ExceptionHandlers;

internal sealed class BadHttpRequestExceptionHandler : IExceptionHandler
{
    private readonly ILogger<BadHttpRequestExceptionHandler> _logger;

    public BadHttpRequestExceptionHandler(ILogger<BadHttpRequestExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BadHttpRequestException badHttpRequestException) return false;

        string errorDetail = badHttpRequestException.Message;

        ValidationErrorDto validationErrorDto = new ValidationErrorDto();
        
        if (errorDetail.Contains("JSON"))
        {
            validationErrorDto.PropertyName = "Request body";
            validationErrorDto.ErrorMessage = "The request body contains invalid JSON";
            validationErrorDto.AttemptedValue = "Request body";
        }
        else if (errorDetail.Contains("Content-Length"))
        {
            validationErrorDto.PropertyName = "Header";
            validationErrorDto.ErrorMessage = "The Content-Length header is invalid";
            validationErrorDto.AttemptedValue = httpContext.Request.Headers["Content-Length"].ToString();;
        }
        else if (errorDetail.Contains("Request body too large"))
        {
            validationErrorDto.PropertyName = "Request body";
            validationErrorDto.ErrorMessage = "The request body exceeds the maximum allowed size";
            validationErrorDto.AttemptedValue = "Too large";
        }
        else if (errorDetail.Contains("Request Aborted"))
        {
            validationErrorDto.PropertyName = "Request";
            validationErrorDto.ErrorMessage = "The request was aborted before completion";
            validationErrorDto.AttemptedValue = null;
        }
        else
        {
            return false;
        }
        
        BaseResponseDto<ValidationErrorDto> response = new BaseResponseDto<ValidationErrorDto>
        {
            Succcess = false,
            Data = validationErrorDto,
            Message = "An error occurred while processing the request"
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}