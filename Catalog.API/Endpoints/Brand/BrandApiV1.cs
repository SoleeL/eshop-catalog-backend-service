using System.ComponentModel.DataAnnotations;
using Catalog.API.Extensions;
using Catalog.Application.Commands.Brands;
using Catalog.Application.Dtos;
using Catalog.Application.DTOs;
using Catalog.Application.Extensions;
using Catalog.Application.Queries.Brands;
using Catalog.Domain.Enums;
using Catalog.Domain.Shared;
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

        BaseResponseDto<IEnumerable<BrandResponseDto>> brandResponseDto = await catalogServices.Mediator.Send(getPageBrandsQuery);

        if (brandResponseDto is { Succcess: true, Data: not null })
        {
            catalogServices.Logger.LogInformation("GetAllBrandsQuery succeeded - Brand obtained");
            catalogServices.HttpContext.PaginateAsync(brandResponseDto.TotalItemCount, getPageBrandsQuery.Page, getPageBrandsQuery.Size);
            return TypedResults.Ok(brandResponseDto);
        }

        catalogServices.Logger.LogWarning("GetAllBrandsQuery failed");
        return TypedResults.BadRequest(brandResponseDto);
    }

    private static IResult GetBrandById(int id) => Results.Ok($"Brand {id} from v1");
}