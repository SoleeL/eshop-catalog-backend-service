using System.Text.Json;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Persistence.Seed;

public static class CategoryDataSeed
{
    public static async Task InitAsync(CatalogDbContext catalogDbContext)
    {
        bool checkCategoriesExists = catalogDbContext.Category.Any();
        if (checkCategoriesExists) return;
        
        string pathDataSeed = Path.Combine(Directory.GetCurrentDirectory(), "Data", "categories.json");
        string categoriesFileData = File.ReadAllText(pathDataSeed);
        
        List<CategoryEntity>? categories = JsonSerializer.Deserialize<List<CategoryEntity>>(categoriesFileData);
        if (categories == null) return;
        
        catalogDbContext.Category.AddRange(categories);
        
        await catalogDbContext.SaveChangesAsync();
    }
}