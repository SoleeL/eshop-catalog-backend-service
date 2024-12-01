using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities;

public class ProductEntity : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    public Guid CategoryId { get; set; }
    public virtual CategoryEntity Category { get; set; }
    
    public Guid TypeId { get; set; }
    public virtual TypeEntity Type { get; set; }
    
    public Guid BrandId { get; set; }
    public virtual BrandEntity Brand { get; set; }
}