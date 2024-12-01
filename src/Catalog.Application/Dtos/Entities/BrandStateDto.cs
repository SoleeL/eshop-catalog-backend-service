namespace Catalog.Application.Dtos.Entities;

public class BrandStateDto : BaseDto<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}