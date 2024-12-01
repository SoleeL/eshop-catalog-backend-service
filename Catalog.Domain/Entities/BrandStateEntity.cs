namespace Catalog.Domain.Entities;

public class BrandStateEntity : BaseEntity<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public virtual ICollection<BrandEntity> brands { get; set; }
}