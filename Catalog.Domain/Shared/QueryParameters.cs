using Catalog.Domain.Enums;

namespace Catalog.Domain.Shared;

public class QueryParameters // Quizas esta clase no es necesaria, y lo mejor es colocar todo en la QUERY y el HANDLER
{
    public bool? Enabled { get; init; } // Filtration
    public Approval? Approval { get; init; } // Filtration
    
    public List<string> Category { get; init; } // Filtration
    public List<string> Brand { get; init; } // Filtration
    
    public string? Search { get; init; } // Search
    
    public int? PriceMin { get; init; } // Range
    public int? PriceMax { get; init; } // Range
    
    public string? DateStart { get; init; } // Range
    public string? DateEnd { get; init; } // Range
    
    public List<string> Sort { get; init; } // Sorting -> ASC = "id", DESC = "-id"

    public int Page { get; init; } = 1; // Pagination
    public int Size { get; init; } = 10; // Pagination
    
    // public List<string> Fields { get; init; } // Projection -> Quizas util mas adelante
    // public List<string> Exclude { get; init; } // Projection -> Quizas util mas adelante
    
    public QueryParameters(
        bool? enabled = null,
        string? approval = null,
        string? category = null,
        string? brand = null,
        
        string? search = null,
        
        int? priceMin = null,
        int? priceMax = null,
        string? dateStart = null,
        string? dateEnd = null,
        
        string? sort = null,
        int? page = null,
        int? size = null
    )
    {
        // Enabled = enabled ?? Enabled;
        // Approval = Enum.TryParse(approval, out Approval parsedApproval) ? parsedApproval : Approval;
        //
        // Category = ParseToList(category);
        // Brand = ParseToList(brand);
        //
        // Search = search;
        //
        // PriceMin = priceMin;
        // PriceMax = priceMax;
        // DateStart = dateStart;
        // DateEnd = dateEnd;
        //
        // Sort = ParseToList(sort);
        //
        // Page = page ?? Page;
        // Size = size ?? Size;
    }


}