using RSS.Domain.Common;

namespace RSS.Domain.Entities;

public class FeedItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Author { get; set; }

    public DateTime PublishDate { get; set; }

    public int ChannelId { get; set; }

    public Channel Channel { get; set; } = default!;

    public List<User> ReadBy { get; set; } = new();
}