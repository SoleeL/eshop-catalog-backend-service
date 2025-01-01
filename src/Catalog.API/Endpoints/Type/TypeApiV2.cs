namespace Catalog.API.Endpoints.Type;

public static class TypeApiV2
{
    public static IEndpointRouteBuilder MapTypeApiV2(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/type").HasApiVersion(2.0);

        api.MapGet("/", GetAllTypes);
        api.MapGet("/{id:int}", GetTypeById);

        return app;
    }

    private static IResult GetAllTypes()
    {
        // Simula la recuperación de datos con una respuesta diferente
        return Results.Ok(new[] { "TypeA", "TypeB", "TypeC" });
    }

    private static IResult GetTypeById(int id)
    {
        // Simula la recuperación de un tipo específico con una respuesta diferente
        return Results.Ok($"Type {id} from v2");
    }
}