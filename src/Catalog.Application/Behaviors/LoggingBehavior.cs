using Catalog.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;   
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
        TResponse response = await next();
        _logger.LogInformation("Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

        return response;
    }
    
    // Al ingresar
    // catalogServices.Logger.LogInformation("Sending command: {CommandName} - {nameProperty}: {CommandId}", // README: ESTO NO INTERESA, YA ESTA EL LOGGER DE COMPORTAMIENTO
    //     brandCreateDto.GetGenericTypeName(), nameof(brandCreateDto.Name), brandCreateDto.Name);

    // Al finalizar
    // if (brandResponseDto is { Succcess: true, Data: not null })
    // {
    //     catalogServices.Logger.LogInformation("CreateBrandCommand succeeded - BrandId: {BrandId}",
    //         brandResponseDto.Data.Id);
    //     return TypedResults.Created($"/api/brand/{brandResponseDto.Data.Id}", brandResponseDto);
    // }
    //
    // catalogServices.Logger.LogWarning("CreateBrandCommand failed");
    // return TypedResults.BadRequest(brandResponseDto);
    
    // AL finalizar la obtencion:
    // if (brandResponseDto is { Succcess: true, Data: not null })
    // {
    //     catalogServices.Logger.LogInformation("GetAllBrandsQuery succeeded - Brand obtained");
    //     catalogServices.HttpContext.PaginateAsync(brandResponseDto.TotalItemCount, getPageBrandsQuery.Page, getPageBrandsQuery.Size);
    //     return TypedResults.Ok(brandResponseDto);
    // }
    //
    // catalogServices.Logger.LogWarning("GetAllBrandsQuery failed");
    // return TypedResults.BadRequest(brandResponseDto);
}

