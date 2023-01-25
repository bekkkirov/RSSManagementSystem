using RSS.Domain.Common;

namespace RSS.Domain.Entities;

public class Channel : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public DateTime? LastBuildDate { get; set; }

    public string? Language { get; set; }

    public string? Copyright { get; set; }

    public ChannelImage? ChannelImage { get; set; }

    public List<FeedItem> Items { get; set; } = new();

    public List<User> SubscribedUsers { get; set; } = new();
}