namespace Catalog.Application.DTOs.Bases;

public class PaginationResponseDto
{
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public int TotalPageCount { get; set; }
    public int TotalItemCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string? PreviousPageUrl { get; set; }
    public string? NextPageUrl { get; set; }
}