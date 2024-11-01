using System.Text.Json;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Persistence.Seed;

public static class TypeDataSeed
{
    public static async Task InitAsync(CatalogDbContext catalogDbContext)
    {
        bool checktypesExists = catalogDbContext.Type.Any();
        if (checktypesExists) return;
        
        string pathDataSeed = Path.Combine(Directory.GetCurrentDirectory(), "Data", "types.json");
        string typesFileData = File.ReadAllText(pathDataSeed);
        
        List<TypeEntity>? types = JsonSerializer.Deserialize<List<TypeEntity>>(typesFileData);
        if (types == null) return;
        
        catalogDbContext.Type.AddRange(types);
        
        await catalogDbContext.SaveChangesAsync();
    }
}