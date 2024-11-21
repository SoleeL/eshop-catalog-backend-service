using Catalog.Domain.Enums;

namespace Catalog.Application.DTOs;

public class BrandResponseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Enabled { get; set; }
    public Approval Approval { get; set; }
    public long CreatedAt { get; set; }
    public long UpdatedAt { get; set; }
}