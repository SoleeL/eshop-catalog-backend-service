using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities;

public class ProductEntity : BaseEntity
{
    [Required] [MaxLength(200)] public string Name { get; set; }

    [MaxLength(500)] public string Description { get; set; }

    [Required] [Range(0, double.MaxValue)] public decimal Price { get; set; }

    [ForeignKey("Category")] public Guid CategoryId { get; set; }

    public virtual CategoryEntity Category { get; set; }

    [ForeignKey("Type")] public Guid TypeId { get; set; }

    public virtual TypeEntity Type { get; set; }

    [ForeignKey("Brand")] public Guid BrandId { get; set; }

    public virtual BrandEntity Brand { get; set; }
}