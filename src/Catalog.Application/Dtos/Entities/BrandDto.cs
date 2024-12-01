namespace Catalog.Application.Dtos.Entities;

public class BrandDto : BaseDto<string>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
    public int StateId { get; set; }
}