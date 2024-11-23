using Catalog.API.ExceptionHandlers;

namespace Catalog.API.Extensions;

public static class ProblemDetailsExtension
{
    public static void AddProblemDetails(this IHostApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<BadHttpRequestExceptionHandler>();
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
    }
}