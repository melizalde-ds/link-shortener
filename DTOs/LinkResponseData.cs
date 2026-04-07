namespace LinkShortener.DTOs;

public class LinkResponseData
{
    public required string OriginalUrl { get; init; }
    public required string ShortUrl { get; init; }
}
