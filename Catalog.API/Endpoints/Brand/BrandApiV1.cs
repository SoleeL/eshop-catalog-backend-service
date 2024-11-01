using Catalog.API.Filters;
using Catalog.Application.Commands;
using Catalog.Application.Commands.Brands;
using Catalog.Application.DTOs;
using Catalog.Application.DTOs.Bases;
using Catalog.Application.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.Brand;

public static class BrandApiV1
{
    public static IEndpointRouteBuilder MapBrandApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/brand").HasApiVersion(1.0);

        api.MapPost("/", CreateBrandAsync);
        
        api.MapGet("/", GetAllBrands);
        
        api.MapGet("/{id:int}", GetBrandById);

        return app;
    }

    private static async Task<IResult> CreateBrandAsync(
        [AsParameters] CatalogServices catalogServices,
        [FromBody] BrandCreateDto brandCreateDto
    )
    {
        catalogServices.Logger.LogInformation(
            "Sending command: {CommandName} - {nameProperty}: {CommandId}",
            brandCreateDto.GetGenericTypeName(), nameof(brandCreateDto.Name), brandCreateDto.Name);
        
        CreateBrandCommand createBrandCommand = new CreateBrandCommand(brandCreateDto.Name);

        // README: Implementando creacion de marca sin idempotencia a traves del CreateBrandCommand
        BaseResponseDto<BrandResponseDto> brandResponseDto = await catalogServices.Mediator.Send(createBrandCommand);

        if (brandResponseDto is { Succcess: true, Data: not null })
        {
            catalogServices.Logger.LogInformation("CreateBrandCommand succeeded - BrandId: {BrandId}", brandResponseDto.Data.Id);
            return TypedResults.Created($"/api/brand/{brandResponseDto.Data.Id}",brandResponseDto);
        }
        
        catalogServices.Logger.LogWarning("CreateBrandCommand failed");
        return TypedResults.BadRequest(brandResponseDto);
    }

    private static IResult GetAllBrands() => Results.Ok(new[] { "Brand1", "Brand2", "Brand3" });

    private static IResult GetBrandById(int id) => Results.Ok($"Brand {id} from v1");
}