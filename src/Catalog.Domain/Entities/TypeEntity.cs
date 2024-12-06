using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities;

public class TypeEntity : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; }
}