namespace RSS.Infrastructure.Options;

public class DbConnectionOptions
{
    public const string SectionName = "ConnectionStrings";

    public string RssContext { get; set; } = string.Empty;

    public string IdentityContext { get; set; } = string.Empty;
}