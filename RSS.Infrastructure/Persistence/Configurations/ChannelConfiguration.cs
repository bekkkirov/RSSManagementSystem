using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.Configurations;

public class ChannelConfiguration : BaseEntityTypeConfiguration<Channel>
{
    public override void Configure(EntityTypeBuilder<Channel> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Title)
               .HasMaxLength(100);

        builder.Property(c => c.Description)
               .HasMaxLength(500);

        builder.Property(c => c.Language)
               .HasMaxLength(10);

        builder.Property(c => c.Copyright)
               .HasMaxLength(50);

        builder.Property(c => c.Link)
               .HasMaxLength(100);

        builder.HasIndex(c => c.Link)
               .IsUnique();

        builder.HasOne(c => c.ChannelImage)
               .WithOne(i => i.Channel)
               .HasForeignKey<ChannelImage>(i => i.ChannelId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}