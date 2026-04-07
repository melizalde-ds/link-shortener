using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Models;

[Index(nameof(ShortUrl), IsUnique = true)]
public class Link
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("original_url")]
    public required string OriginalUrl { get; set; }

    [JsonPropertyName("short_url")]
    public required string ShortUrl { get; set; }
}
