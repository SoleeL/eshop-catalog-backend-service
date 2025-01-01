namespace Catalog.Domain.Entities;

public class BrandEntity : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
    
    public int StateId { get; set; }
    public virtual BrandStateEntity State { get; set; }
    
    public virtual ICollection<ProductEntity> Products { get; set; }
}
