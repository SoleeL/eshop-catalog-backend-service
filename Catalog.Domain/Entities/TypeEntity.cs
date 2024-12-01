using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities;

public class TypeEntity : BaseEntity<Guid>
{
    [Required] [MaxLength(100)] public string Name { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; }
}