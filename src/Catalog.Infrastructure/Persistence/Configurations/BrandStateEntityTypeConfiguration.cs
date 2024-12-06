using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class BrandStateEntityTypeConfiguration : BaseEntityTypeConfiguration<BrandStateEntity>
{
    public override void Configure(EntityTypeBuilder<BrandStateEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("brand_states");
        
        builder.HasKey(e => e.Id); // Primary key
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(b => b.Name)
            .IsUnique();
        
        builder.Property(b => b.Description)
            .HasColumnName("description")
            .HasMaxLength(255);
    }
}