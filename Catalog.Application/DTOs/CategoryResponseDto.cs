namespace Catalog.Application.DTOs;

public class CategoryResponseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public long CreatedAt { get; set; }
    public long UpdatedAt { get; set; }
}