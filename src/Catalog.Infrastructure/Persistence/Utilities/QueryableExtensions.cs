namespace Catalog.Infrastructure.Persistence.Utilities;

public static class QueryableExtensions
{
    public static IQueryable<T> PageBy<T>(this IQueryable<T> queryable, int page, int size)
    {
        return queryable
            .Skip((page - 1) * size)
            .Take(size);
    }
}