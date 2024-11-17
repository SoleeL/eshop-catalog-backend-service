using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities;

public class BrandEntity : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; }
}