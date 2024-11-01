using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.Configurations;

public class TypeEntityTypeConfiguration : IEntityTypeConfiguration<TypeEntity>
{
    public void Configure(EntityTypeBuilder<TypeEntity> builder)
    {
        
    }
}