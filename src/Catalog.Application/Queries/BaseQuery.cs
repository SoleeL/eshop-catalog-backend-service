using MediatR;

namespace Catalog.Application.Queries;

public abstract class BaseQuery<TResponse> : IRequest<TResponse>
{
    protected List<string> ParseToList(string? parameters)
    {
        if (parameters == null) return new List<string>();

        return parameters.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(value => value.Trim()) // Eliminar espacios
            .Select(Capitalize) // Capitalizar los valores
            .Where(value => value != null) // Filtrar los valores null
            .Cast<string>() // Garantizar que el tipo sea IEnumerable<string>
            .ToList();
    }

    protected string? Capitalize(string? toCapitalize)
    {
        if (toCapitalize is null) return toCapitalize;
        
        int firstChart = toCapitalize.StartsWith("-") ? 1 : 0;
        string capitalized = char.ToUpperInvariant(toCapitalize[firstChart]) + toCapitalize.Substring(firstChart + 1).ToLowerInvariant();
        
        return toCapitalize.StartsWith("-") ? "-" + capitalized : capitalized; 
    }
}