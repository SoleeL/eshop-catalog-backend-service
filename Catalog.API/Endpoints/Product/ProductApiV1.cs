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

    private static IResult GetAllProductsV1() => Results.Ok(new[] { "Product1", "Product2", "Product3" });
    
    private static IResult GetProductByIdV1(int id) => Results.Ok($"Product {id} from v1");
}