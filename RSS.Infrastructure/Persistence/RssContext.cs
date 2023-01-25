using Microsoft.EntityFrameworkCore;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence;

public class RssContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;

    public DbSet<ChannelImage> ChannelImages { get; set; } = default!;

    public DbSet<Channel> Channels { get; set; } = default!;

    public DbSet<FeedItem> FeedItems { get; set; } = default!;

    public RssContext(DbContextOptions<RssContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RssContext).Assembly);
    }
}