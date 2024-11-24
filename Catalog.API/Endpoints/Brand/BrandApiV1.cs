using Catalog.API.Extensions;
using Catalog.Application.Commands.Brands;
using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Extensions;
using Catalog.Application.Queries.Brands;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.Brand;

public static class BrandApiV1
{
    public static IEndpointRouteBuilder MapBrandApiV1(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder api = app.MapGroup("/api/brand").HasApiVersion(1.0);

        api.MapPost("/", CreateBrandAsync);

        api.MapGet("/", GetPageBrands);

        api.MapGet("/{id:int}", GetBrandById);

        return app;
    }

    private static async Task<IResult> CreateBrandAsync(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromBody] CreateBrandCommand createBrandCommand
    )
    {
        // README: Implementando creacion de marca sin idempotencia a traves del CreateBrandCommand
        BaseResponseDto<BrandResponseDto> brandResponseDto = await catalogServices.Mediator.Send(createBrandCommand, cancellationToken);
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
        [FromQuery] string? approval,
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] int? page,
        [FromQuery] int? size
    )
    {
        GetPageBrandsQuery getPageBrandsQuery = new GetPageBrandsQuery(
            enabled: enabled,
            approval: approval,
            search: search,
            sort: sort,
            page: page,
            size: size
        );

        BaseResponseDto<IEnumerable<BrandResponseDto>> brandResponseDto = await catalogServices.Mediator.Send(getPageBrandsQuery, cancellationToken);
        catalogServices.HttpContext.PaginateAsync(brandResponseDto.TotalItemCount, getPageBrandsQuery.Page, getPageBrandsQuery.Size);
        return TypedResults.Ok(brandResponseDto);
    }

    private static IResult GetBrandById(int id) => Results.Ok($"Brand {id} from v1");
}