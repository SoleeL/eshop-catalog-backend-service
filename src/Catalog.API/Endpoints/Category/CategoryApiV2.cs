namespace Catalog.API.Endpoints.Category;

public static class CategoryApiV2
{
    public static IEndpointRouteBuilder MapCategoryApiV2(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/category").HasApiVersion(2.0);

        // Mapeo de rutas para la versión 2
        api.MapGet("/", GetAllCategories);
        api.MapGet("/{id:int}", GetCategoryById);
        api.MapPost("/", CreateCategory);
        api.MapPut("/{id:int}", UpdateCategory);
        api.MapDelete("/{id:int}", DeleteCategory);

        return app;
    }

    private static IResult GetAllCategories() 
    {
        // Lógica para obtener todas las categorías de la versión 2
        return Results.Ok(new[] { "CategoryA", "CategoryB", "CategoryC" });
    }

    private static IResult GetCategoryById(int id) 
    {
        // Lógica para obtener una categoría por ID de la versión 2
        return Results.Ok($"Category {id} from v2");
    }

    private static IResult CreateCategory(Catalog.API.Endpoints.Category.Category category) 
    {
        // Lógica para crear una nueva categoría de la versión 2
        return Results.Created($"/api/category/{category.Id}", category);
    }

    private static IResult UpdateCategory(int id, Catalog.API.Endpoints.Category.Category category) 
    {
        // Lógica para actualizar una categoría por ID de la versión 2
        return Results.Ok($"Category {id} updated to {category.Name}");
    }

    private static IResult DeleteCategory(int id) 
    {
        // Lógica para eliminar una categoría por ID de la versión 2
        return Results.NoContent(); // No hay contenido para devolver después de eliminar
    }
}