using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public abstract class BaseEntityTypeConfiguration<T, TKey> : IEntityTypeConfiguration<T> where T : BaseEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id); // Primary key

        // builder.Property(e => e.Id)
        //     .HasColumnName("id")
        //     .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()")
            .IsRequired()
            // README: Generar al crear la entidad
            // .ValueGeneratedOnAdd()
            // Ignorar campo en modificacion/update
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("now()")
            .IsRequired()
            // README: Generar al crear la entidad o actualizar
            // .ValueGeneratedOnAddOrUpdate()
            // Ignorar campo en modificacion/update
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
    }
}