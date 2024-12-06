namespace Catalog.Application.Dtos.Entities;

public class BrandStateDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}