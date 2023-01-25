using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.Configurations;

public class UserConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.UserName)
               .HasMaxLength(30);
        
        builder.HasIndex(u => u.UserName)
               .IsUnique();

        builder.HasMany(u => u.Channels)
               .WithMany(c => c.SubscribedUsers)
               .UsingEntity(join => join.ToTable("UserChannels"));

        builder.HasMany(u => u.ReadNews)
               .WithMany(i => i.ReadBy)
               .UsingEntity(join => join.ToTable("UserReadNews"));
    }
}