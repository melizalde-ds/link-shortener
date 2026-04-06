namespace LinkShortener.Models;

public class Link
{
    public int Id { get; init; }
    public required string OriginalUrl { get; set; }
    public required string ShortUrl { get; set; }
}