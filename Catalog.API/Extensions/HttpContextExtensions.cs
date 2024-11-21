using System.Text.Json;
using Catalog.Application.Dtos;

namespace Catalog.API.Extensions;

public static class HttpContextExtensions
{
    public static void PaginateAsync(
        this HttpContext httpContext,
        int totalItemCount,
        int currentPage, 
        int pageSize
    )
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        
        int totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        
        // Construir URL de la página anterior y siguiente
        var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}";
        Boolean hasPrevious = currentPage > 1;
        Boolean hasNext = currentPage < totalPageCount;
        var previousPageUrl = hasPrevious ? $"{baseUrl}?page={currentPage - 1}&size={pageSize}" : null;
        var nextPageUrl = hasNext ? $"{baseUrl}?page={currentPage + 1}&size={pageSize}" : null;
        
        PaginationResponseDto paginationResponseDto = new PaginationResponseDto()
        {
            HasPrevious = hasPrevious,
            HasNext = hasNext,
            TotalPageCount = totalPageCount,
            TotalItemCount = totalItemCount,
            CurrentPage = currentPage,
            PageSize = pageSize,
            PreviousPageUrl = previousPageUrl,
            NextPageUrl = nextPageUrl
        };
        
        // Esta reemplazando & por \u0026
        httpContext.Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationResponseDto));
    }
}