namespace Catalog.Domain.Entities;

public class BrandStateEntity : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public virtual ICollection<BrandEntity> Brands { get; set; }
}