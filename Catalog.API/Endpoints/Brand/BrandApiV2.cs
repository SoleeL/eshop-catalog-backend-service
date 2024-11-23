using Catalog.API.Filters;
using Catalog.Application.Commands;
using Catalog.Application.Commands.Brands;
using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.Brand;

public static class BrandApiV2
{
    public static IEndpointRouteBuilder MapBrandApiV2(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/brand").HasApiVersion(2.0);

        api.MapPost("/", CreateBrandAsync)
            .AddEndpointFilter<RequestIdFilter>();
        
        api.MapGet("/", GetAllBrands);
        api.MapGet("/{id:int}", GetBrandById);

        return app;
    }

    private static async Task<IResult> CreateBrandAsync(
        [AsParameters] CatalogServices catalogServices,
        [FromHeader(Name = "X-RequestId")] Guid? requestGuid,
        [FromBody] BrandCreateDto brandCreateDto
    )
    {
        catalogServices.Logger.LogInformation(
            "Sending command: {CommandName} - {nameProperty}: {CommandId}",
            brandCreateDto.GetGenericTypeName(), nameof(brandCreateDto.Name), brandCreateDto.Name);
        
        CreateBrandCommand createBrandCommand = new CreateBrandCommand(brandCreateDto.Name, brandCreateDto.Description);

        // README: Implementando creacion de marca utilizando idempotencia a traves del IdentifiedCommand
        IdentifiedCommand<CreateBrandCommand, BaseResponseDto<BrandResponseDto>> requestCreateBrand =
            new IdentifiedCommand<CreateBrandCommand, BaseResponseDto<BrandResponseDto>>(createBrandCommand, requestGuid.Value);

        catalogServices.Logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            requestCreateBrand.GetGenericTypeName(),
            nameof(requestCreateBrand.Id),
            requestCreateBrand.Id,
            requestCreateBrand);

        BaseResponseDto<BrandResponseDto> brandResponseDto = await catalogServices.Mediator.Send(requestCreateBrand);

        if (brandResponseDto is { Succcess: true, Data: not null })
        {
            catalogServices.Logger.LogInformation("CreateBrandCommand succeeded - BrandId: {BrandId}", brandResponseDto.Data.Id);
            return TypedResults.Created($"/api/brand/{brandResponseDto.Data.Id}",brandResponseDto);
        }
        
        catalogServices.Logger.LogWarning("CreateBrandCommand failed");
        return TypedResults.BadRequest(brandResponseDto);
    }
    
    private static IResult GetAllBrands() => Results.Ok(new[] { "BrandA", "BrandB", "BrandC" });
    
    private static IResult GetBrandById(int id) => Results.Ok($"Brand {id} from v2");
}