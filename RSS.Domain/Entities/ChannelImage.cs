using RSS.Domain.Common;

namespace RSS.Domain.Entities;

public class ChannelImage : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int ChannelId { get; set; }

    public Channel Channel { get; set; } = default!;
}