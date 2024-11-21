using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public class BrandEntity : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
    public int Approval { get; set; }
    
    public virtual ICollection<ProductEntity> Products { get; set; }
}
