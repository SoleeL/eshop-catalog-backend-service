namespace Catalog.API.Endpoints.Type;

public static class TypeApiV1
{
    public static IEndpointRouteBuilder MapTypeApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/type").HasApiVersion(1.0);

        api.MapGet("/", GetAllTypes);
        api.MapGet("/{id:int}", GetTypeById);

        return app;
    }

    private static IResult GetAllTypes()
    {
        // Simula la recuperación de datos
        return Results.Ok(new[] { "Type1", "Type2", "Type3" });
    }

    private static IResult GetTypeById(int id)
    {
        // Simula la recuperación de un tipo específico
        return Results.Ok($"Type {id} from v1");
    }
}