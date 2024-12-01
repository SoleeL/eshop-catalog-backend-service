namespace Catalog.Application.Validations.Utils;

public class ValidFieldsUtil
{
    public static HashSet<string> GetValidFields<T>()
    {
        return typeof(T)
            .GetProperties()
            .SelectMany(p => new[] { p.Name, $"-{p.Name}" })
            .ToHashSet();
    }
}