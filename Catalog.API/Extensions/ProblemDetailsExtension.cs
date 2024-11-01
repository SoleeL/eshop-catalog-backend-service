using Catalog.API.Exceptions;

namespace Catalog.API.Extensions;

public static class ProblemDetailsExtension
{
    public static void AddProblemDetails(this IHostApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
    }
    
}