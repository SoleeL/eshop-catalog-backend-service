namespace Catalog.API.Endpoints.Product;

public static class ProductApiV2
{
    public static IEndpointRouteBuilder MapProductApiV2(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/product").HasApiVersion(2.0);

        api.MapGet("/", GetAllProductsV2);
        api.MapGet("/{id:int}", GetProductByIdV2);

        return app;
    }

    private static IResult GetAllProductsV2() => Results.Ok(new[] { "ProductA", "ProductB", "ProductC" });
    
    private static IResult GetProductByIdV2(int id) => Results.Ok($"Product {id} from v2");
}