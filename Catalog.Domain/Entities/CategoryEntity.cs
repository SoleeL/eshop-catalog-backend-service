using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities;

public class CategoryEntity : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; }
}