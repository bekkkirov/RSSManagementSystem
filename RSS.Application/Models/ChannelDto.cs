namespace RSS.Application.Models;

public class ChannelDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public DateTime? LastBuildDate { get; set; }

    public string? Language { get; set; }

    public string? Copyright { get; set; }

    public ChannelImageDto? ChannelImage { get; set; }
}