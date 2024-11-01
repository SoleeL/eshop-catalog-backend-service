namespace Catalog.API.Endpoints.Category;

public static class CategoryApiV1
{
    public static IEndpointRouteBuilder MapCategoryApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/category").HasApiVersion(1.0);

        // Mapeo de rutas para la versión 1
        api.MapGet("/", GetAllCategories);
        api.MapGet("/{id:int}", GetCategoryById);
        api.MapPost("/", CreateCategory);

        return app;
    }

    private static IResult GetAllCategories() 
    {
        // Lógica para obtener todas las categorías de la versión 1
        return Results.Ok(new[] { "Category1", "Category2", "Category3" });
    }

    private static IResult GetCategoryById(int id) 
    {
        // Lógica para obtener una categoría por ID de la versión 1
        return Results.Ok($"Category {id} from v1");
    }

    private static IResult CreateCategory(Category category) 
    {
        // Lógica para crear una nueva categoría de la versión 1
        return Results.Created($"/api/category/{category.Id}", category);
    }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}