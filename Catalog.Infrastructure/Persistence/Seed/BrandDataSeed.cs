using System.Text.Json;
using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Persistence.Seed;

public static class BrandDataSeed
{
    public static async Task InitAsync(CatalogDbContext catalogDbContext)
    {
        bool checkBrandsExists = catalogDbContext.Brand.Any();
        if (checkBrandsExists) return;
        
        string pathDataSeed = Path.Combine(Directory.GetCurrentDirectory(), "Data", "brands.json");
        string brandsFileData = File.ReadAllText(pathDataSeed);
        
        List<BrandEntity>? brands = JsonSerializer.Deserialize<List<BrandEntity>>(brandsFileData);
        if (brands == null) return;
        
        catalogDbContext.Brand.AddRange(brands);
        
        await catalogDbContext.SaveChangesAsync();
    }
}