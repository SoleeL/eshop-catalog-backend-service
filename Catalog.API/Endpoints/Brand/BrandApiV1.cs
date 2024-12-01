using Catalog.API.Extensions;
using Catalog.Application.Commands.Brands;
using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Queries.Brands;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.Brand;

public static class BrandApiV1
{
    public static IEndpointRouteBuilder MapBrandApiV1(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder api = app.MapGroup("/api/brand").HasApiVersion(1.0);

        api.MapPost("/", CreateBrand);

        api.MapGet("/", GetPageBrands);

        api.MapGet("/{guid:Guid}", GetBrandById);

        // api.MapPut("/{id:int}", ChangeBrandById); // Actualización completa del recurso

        api.MapPatch("/{guid:Guid}", UpdateBrandById); // Actualización parcial del recurso

        api.MapDelete("/{guid:Guid}", DeleteBrandById);

        return app;
    }

    private static async Task<IResult> CreateBrand(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromBody] CreateBrandCommand createBrandCommand
    )
    {
        // README: Implementando creacion de marca sin idempotencia a traves del CreateBrandCommand
        BaseResponseDto<BrandDto> brandResponseDto = await catalogServices.Mediator.Send(
            createBrandCommand, 
            cancellationToken);
        // IMPORTANT: Sobre metodo .Send(
        // La definicion del metodo .Send( es:
        // public abstract Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        // 1.- request es nuestro comando o query
        // 2.- cancellationToken es un parámetro opcional
        //      default(CancellationToken):
        //          * Cuando no se pasa un valor explícito, el compilador genera un CancellationToken por defecto.
        //          * Un CancellationToken por defecto es un token no cancelable (es decir, IsCancellationRequested siempre será false).
        //          * Esto significa que el método funcionará sin interrupciones porque nunca se solicitará la cancelación.
        //      Si queremos poder evitar que un proceso se finalice si la request fue cancelada o descartada por el
        //          cliente, se debe ser explícito al pasar el CancellationToken desde el endpoint a nuestro handler.

        return TypedResults.Created($"/api/brand/{brandResponseDto.Data.Id}", brandResponseDto);
    }

    private static async Task<IResult> GetPageBrands(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromQuery] bool? enabled,
        [FromQuery] int? stateId,
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] int? page,
        [FromQuery] int? size
    )
    {
        GetPageBrandsQuery getPageBrandsQuery = new GetPageBrandsQuery(
            enabled: enabled,
            state: stateId,
            search: search,
            sort: sort,
            page: page,
            size: size
        );

        BaseResponseDto<IEnumerable<BrandDto>> brandResponseDto = await catalogServices.Mediator.Send(
            getPageBrandsQuery,
            cancellationToken);

        catalogServices.HttpContext.PaginateAsync(
            brandResponseDto.TotalItemCount,
            getPageBrandsQuery.Page,
            getPageBrandsQuery.Size);

        return TypedResults.Ok(brandResponseDto);
    }

    private static async Task<IResult> GetBrandById(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromRoute] Guid guid
    )
    {
        GetBrandByIdQuery getBrandByIdQuery = new GetBrandByIdQuery(guid);

        BaseResponseDto<BrandDto> brandResponseDto = await catalogServices.Mediator.Send(
            getBrandByIdQuery,
            cancellationToken);

        if (brandResponseDto.Succcess == false) return TypedResults.NotFound(brandResponseDto);
        
        return TypedResults.Ok(brandResponseDto);
    }
    
    // Respuestas esperadas para el método de actualización (PUT o PATCH).
    // Casos y códigos de estado:
    // 1.- Actualización exitosa: 200 OK - El recurso fue actualizado y se devuelve en la respuesta.
    // 2.- No encontrado: 404 Not Found - El recurso solicitado no existe.
    // 3.- Datos inválidos: 400 Bad Request - Los datos proporcionados en la solicitud no son válidos.
    // 4.- Conflicto: 409 Conflict - Violación de reglas del negocio o estado inconsistente.
    private static async Task<IResult> UpdateBrandById(        
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromRoute] Guid guid,
        [FromBody] UpdateBrandCommand updateBrandCommand
    )
    {
        updateBrandCommand.Guid = guid;
        BaseResponseDto<BrandDto> brandResponseDto = await catalogServices.Mediator.Send(
            updateBrandCommand,
            cancellationToken);

        if (brandResponseDto.Succcess == false) return TypedResults.NotFound(brandResponseDto);
        
        return TypedResults.Ok(brandResponseDto);
    }
    
    private static async Task<IResult> DeleteBrandById(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromRoute] Guid guid
    )
    {
        DeleteBrandCommand deleteBrandCommand = new DeleteBrandCommand(guid);

        BaseResponseDto<BrandDto> brandResponseDto = await catalogServices.Mediator.Send(
            deleteBrandCommand,
            cancellationToken);

        if (brandResponseDto.Succcess == false) return TypedResults.NotFound(brandResponseDto);
        
        return TypedResults.NoContent();
    }
}