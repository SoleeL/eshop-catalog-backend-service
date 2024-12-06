using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public class BrandEntityTypeConfiguration : BaseEntityTypeConfiguration<BrandEntity>
{
    public override void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        base.Configure(builder); // README: Llama a la configuración de BaseEntity

        builder.ToTable("brands");

        builder.Property(e => e.Id) // Por conflictos con el tipo de id
            .HasColumnName("id")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(b => b.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(b => b.Name)
            .IsUnique();

        builder.Property(b => b.Description)
            .HasColumnName("description")
            .HasMaxLength(255);

        builder.Property(b => b.Enabled)
            .HasColumnName("enabled")
            .IsRequired()
            .HasDefaultValueSql("TRUE");
        
        builder.Property(b => b.StateId)
            .HasColumnName("state_id");
        builder.HasOne(b => b.State)
            .WithMany(s => s.Brands) // Define explícitamente la colección
            .HasForeignKey(b => b.StateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}