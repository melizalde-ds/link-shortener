using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Models;

[Index(nameof(ShortUrl), IsUnique = true)]
public class Link
{
    [JsonPropertyName("original_url")]
    public required string OriginalUrl { get; set; }

    [Key]
    [JsonPropertyName("short_url")]
    public required string ShortUrl { get; set; }

    public static string GenerateShortUrl()
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(Chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
