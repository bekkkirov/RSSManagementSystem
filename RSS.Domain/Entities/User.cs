using RSS.Domain.Common;

namespace RSS.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;

    public List<Channel> Channels { get; set; } = new();

    public List<FeedItem> ReadNews { get; set; } = new();
}