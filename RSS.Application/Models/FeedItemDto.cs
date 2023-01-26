namespace RSS.Application.Models;

public class FeedItemDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Author { get; set; }

    public DateTime PublishDate { get; set; }

    public int ChannelId { get; set; }
}