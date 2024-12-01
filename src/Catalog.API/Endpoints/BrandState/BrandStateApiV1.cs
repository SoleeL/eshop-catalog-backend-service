using Catalog.Application.Commands.BrandStates;
using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Queries.BrandStates;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.BrandState;

public static class BrandStateApiV1
{
    public static IEndpointRouteBuilder MapBrandStateApiV1(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder api = app.MapGroup("/api/brandstatus").HasApiVersion(1.0);

        api.MapPost("/", CreateBrandState);

        api.MapGet("/", GetPageBrandStates);

        api.MapGet("/{id:int}", GetBrandStateById);

        // api.MapPut("/{id:int}", ChangeBrandStateById); // Actualización completa del recurso

        api.MapPatch("/{id:int}", UpdateBrandStateById);

        api.MapDelete("/{id:int}", DeleteBrandStateById);

        return app;
    }

    private static async Task<IResult> CreateBrandState(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromBody] CreateBrandStateCommand createBrandStateCommand
    )
    {
        BaseResponseDto<BrandStateDto> brandStateResponseDto = await catalogServices.Mediator.Send(
            createBrandStateCommand,
            cancellationToken);

        return TypedResults.Created($"/api/brandstate/{brandStateResponseDto.Data.Id}", brandStateResponseDto);
    }

    private static async Task<IResult> GetPageBrandStates(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices
        // README: NO REQUIERE DE PARAMETROS DE QUERY, SE INFIERE QUE SON POCOS LOS POSIBLES BRANDSTATES
    )
    {
        BaseResponseDto<IEnumerable<BrandStateDto>> brandStatesResponseDto = await catalogServices.Mediator.Send(
            new GetBrandStatesQuery(), cancellationToken);

        return TypedResults.Ok(brandStatesResponseDto);
    }

    private static async Task<IResult> GetBrandStateById(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromRoute] int id
    )
    {
        GetBrandStateByIdQuery getBrandStateByIdQuery = new GetBrandStateByIdQuery(id);

        BaseResponseDto<BrandStateDto> brandStateResponseDto = await catalogServices.Mediator.Send(
            getBrandStateByIdQuery, cancellationToken);

        if (brandStateResponseDto.Succcess == false) return TypedResults.NotFound(brandStateResponseDto);

        return TypedResults.Ok(brandStateResponseDto);
    }

    private static async Task<IResult> UpdateBrandStateById(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromRoute] int id,
        [FromBody] UpdateBrandStateCommand updateBrandStateCommand
    )
    {
        updateBrandStateCommand.Id = id;
        BaseResponseDto<BrandStateDto> brandStateResponseDto = await catalogServices.Mediator.Send(
            updateBrandStateCommand,
            cancellationToken);

        if (brandStateResponseDto.Succcess == false) return TypedResults.NotFound(brandStateResponseDto);
        
        return TypedResults.Ok(brandStateResponseDto);
    }

    private static async Task<IResult> DeleteBrandStateById(
        CancellationToken cancellationToken,
        [AsParameters] CatalogServices catalogServices,
        [FromRoute] int id
    )
    {
        DeleteBrandStateCommand deleteBrandStateCommand = new DeleteBrandStateCommand(id);
        
        BaseResponseDto<BrandStateDto> brandStateResponseDto = await catalogServices.Mediator.Send(
            deleteBrandStateCommand, cancellationToken);
        
        if (brandStateResponseDto.Succcess == false) return TypedResults.NotFound(brandStateResponseDto);
        
        return TypedResults.NoContent();
    }
}