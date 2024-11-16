using Catalog.Infrastructure.Persistence.Seed;

namespace Catalog.Infrastructure.Persistence;

public class CatalogDataSeedContext
{
    public static async Task InitAsync(CatalogDbContext context)
    {
        await BrandDataSeed.InitAsync(context);
        await CategoryDataSeed.InitAsync(context);
        await TypeDataSeed.InitAsync(context);
        await ProductDataSeed.InitAsync(context);
    }
}