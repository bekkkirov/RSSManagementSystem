using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSS.Domain.Common;

namespace RSS.Infrastructure.Persistence.Configurations;

public class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity: BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }
}