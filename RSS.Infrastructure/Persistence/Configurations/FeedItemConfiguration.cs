using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.Configurations;

public class FeedItemConfiguration : BaseEntityTypeConfiguration<FeedItem>
{
    public override void Configure(EntityTypeBuilder<FeedItem> builder)
    {
        base.Configure(builder);

        builder.Property(i => i.Link)
               .HasMaxLength(100);

        builder.Property(i => i.Description)
               .HasMaxLength(500);
        
        builder.Property(i => i.Author)
               .HasMaxLength(30);

        builder.HasOne(i => i.Channel)
               .WithMany(c => c.Items)
               .HasForeignKey(i => i.ChannelId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}