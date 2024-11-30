using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

// TODO: Revisar si se puede implementar una configuracion a BaseEntity

public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<BrandEntity>
{
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        builder.ToTable("brands");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
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
            .IsRequired();
        
        builder.Property(b => b.Approval)
            .HasColumnName("approval")
            .IsRequired();
        
        builder.Property(b => b.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()")
            .IsRequired()
            // README: Generar al crear la entidad
            // .ValueGeneratedOnAdd()
            // Ignorar campo en modificacion/update
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

        builder.Property(b => b.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("now()")
            .IsRequired()
            // README: Generar al crear la entidad o actualizar
            // .ValueGeneratedOnAddOrUpdate()
            // Ignorar campo en modificacion/update
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore); 
    }
}