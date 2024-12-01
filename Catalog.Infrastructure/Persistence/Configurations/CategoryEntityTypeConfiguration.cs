using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class CategoryEntityTypeConfiguration : BaseEntityTypeConfiguration<CategoryEntity, Guid>
{
    public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("categories");
        
        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(c => c.Name)
            .IsUnique();
        
        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(255);
        
        builder.Property(c => c.Enabled)
            .HasColumnName("enabled")
            .IsRequired()
            .HasDefaultValueSql("TRUE");
    }
}