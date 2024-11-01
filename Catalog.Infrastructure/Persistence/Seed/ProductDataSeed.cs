using System.Text.Json;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Persistence.Seed;

public static class ProductDataSeed
{
    public static async Task InitAsync(CatalogDbContext catalogDbContext)
    {
        bool checkProductsExists = catalogDbContext.Product.Any();
        if (checkProductsExists) return;
        
        string pathDataSeed = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.json");
        string productsFileData = File.ReadAllText(pathDataSeed);
        
        List<ProductEntity>? products = JsonSerializer.Deserialize<List<ProductEntity>>(productsFileData);
        if (products == null) return;
        
        catalogDbContext.Product.AddRange(products);
        
        await catalogDbContext.SaveChangesAsync();
    }
}