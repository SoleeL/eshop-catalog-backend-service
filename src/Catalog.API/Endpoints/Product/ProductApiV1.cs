using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Endpoints.Product;

public static class ProductApiV1
{
    public static IEndpointRouteBuilder MapProductApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/product").HasApiVersion(1.0);

        api.MapGet("/", GetAllProductsV1);
        api.MapGet("/{id:int}", GetProductByIdV1);

        return app;
    }

    private static IResult GetAllProductsV1(
        [FromQuery] int page = 1, // Pagination
        [FromQuery] int size = 10, // Pagination
        [FromQuery] int? limit = null, // Limitation
        [FromQuery] int? offset = null, // Limitation
        [FromQuery] string? sort = null, // Sorting -> ASC = "id", DESC = "-id"
        [FromQuery] string? status = null, // Filtration
        [FromQuery] string? category = null, // Filtration
        [FromQuery] string? brand = null, // Filtration
        [FromQuery] int? priceMin = null, // Range
        [FromQuery] int? price_max = null, // Range
        [FromQuery] string? date_start = null, // Range
        [FromQuery] string? date_end = null, // Range
        [FromQuery] string? search = null, // Search
        [FromQuery] string? fields = null, // Projection
        [FromQuery] string? exclude = null // Projection
    )
    {
        return Results.Ok(new[] { "Product1", "Product2", "Product3" });
    }
    
    private static IResult GetProductByIdV1(int id) => Results.Ok($"Product {id} from v1");
}