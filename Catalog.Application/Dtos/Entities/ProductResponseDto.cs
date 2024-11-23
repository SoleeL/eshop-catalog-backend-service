namespace Catalog.Application.Dtos.Entities;

public class ProductResponseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public BrandResponseDto Brand { get; set; }
    public CategoryResponseDto Category { get; set; }
    public TypeResponseDto Type { get; set; }
    public long CreatedAt { get; set; }
    public long UpdatedAt { get; set; }
}