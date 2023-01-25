using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.Configurations;

public class ChannelImageConfiguration : BaseEntityTypeConfiguration<ChannelImage>
{
    public override void Configure(EntityTypeBuilder<ChannelImage> builder)
    {
        base.Configure(builder);

        builder.Property(i => i.Title)
               .HasMaxLength(50);

        builder.Property(i => i.Description)
               .HasMaxLength(100);

        builder.Property(i => i.Link)
               .HasMaxLength(100);
    }
}