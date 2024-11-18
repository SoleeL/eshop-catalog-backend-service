using Catalog.API.Extensions;
using Catalog.Application.Commands.Brands;
using Catalog.Application.DTOs;
using Catalog.Application.DTOs.Bases;
using Catalog.Application.Extensions;
using Catalog.Application.Queries.Brands;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.Brand;

public static class BrandApiV1
{
    public static IEndpointRouteBuilder MapBrandApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/brand").HasApiVersion(1.0);

        api.MapPost("/", CreateBrandAsync);

        api.MapGet("/", GetPageBrands);

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

        CreateBrandCommand createBrandCommand = new CreateBrandCommand(brandCreateDto.Name, brandCreateDto.Description);

        // README: Implementando creacion de marca sin idempotencia a traves del CreateBrandCommand
        BaseResponseDto<BrandResponseDto> brandResponseDto = await catalogServices.Mediator.Send(createBrandCommand);

        if (brandResponseDto is { Succcess: true, Data: not null })
        {
            catalogServices.Logger.LogInformation("CreateBrandCommand succeeded - BrandId: {BrandId}",
                brandResponseDto.Data.Id);
            return TypedResults.Created($"/api/brand/{brandResponseDto.Data.Id}", brandResponseDto);
        }

        catalogServices.Logger.LogWarning("CreateBrandCommand failed");
        return TypedResults.BadRequest(brandResponseDto);
    }

    private static async Task<IResult> GetPageBrands(
        [AsParameters] CatalogServices catalogServices,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10
    )
    {
        GetPageBrandsQuery getAllBrandsQuery = new GetPageBrandsQuery(page, size);
        
        (BaseResponseDto<IEnumerable<BrandResponseDto>> brandResponseDto, int totalItemCount) = await catalogServices.Mediator.Send(getAllBrandsQuery);
        
        if (brandResponseDto is { Succcess: true, Data: not null })
        {
            catalogServices.Logger.LogInformation("GetAllBrandsQuery succeeded - Brand obtained");
            catalogServices.HttpContext.PaginateAsync(totalItemCount, page, size);
            return TypedResults.Ok(brandResponseDto);
        }

        catalogServices.Logger.LogWarning("GetAllBrandsQuery failed");
        return TypedResults.BadRequest(brandResponseDto);
    }

    private static IResult GetBrandById(int id) => Results.Ok($"Brand {id} from v1");
}