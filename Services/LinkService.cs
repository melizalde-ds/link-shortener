using System.Text.Json;
using LinkShortener.Data;
using LinkShortener.DTOs;
using LinkShortener.Models;
using LinkShortener.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace LinkShortener.Service;

/// <summary>
/// Handles link resolution with Redis caching and PostgreSQL fallback.
/// </summary>
/// <param name="redis">The Redis cache instance for fast lookups.</param>
/// <param name="db">The PostgreSQL database context for persistent storage.</param>
public class LinkService(IDistributedCache redis, AppDbContext db)
{
    private readonly IDistributedCache _redis = redis;
    private readonly AppDbContext _db = db;

    private static DistributedCacheEntryOptions CacheOptions =>
        new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(30));

    /// <summary>
    /// Resolves a short URL to its full link, checking Redis cache first and falling back to PostgreSQL.
    /// </summary>
    /// <param name="url">The short URL identifier to look up.</param>
    /// <returns>The matching <see cref="Link"/> if found; otherwise, <see langword="null"/>.</returns>
    public async Task<Link?> GetLinkAsync(string url)
    {
        var json = await _redis.GetStringAsync(url);
        if (json != null)
        {
            return JsonSerializer.Deserialize<Link>(json);
        }

        var link = await _db.Links.FirstOrDefaultAsync(l => l.ShortUrl == url);
        if (link != null)
        {
            json = JsonSerializer.Serialize(link);
            await _redis.SetStringAsync(url, json, CacheOptions);
            return link;
        }

        return null;
    }

    /// <summary>
    /// Creates a new <see cref="Link"/> with a unique short URL generated from the provided original URL and a counter for uniqueness.
    /// </summary>
    /// <param name="originalUrl">
    /// The original URL to be shortened.
    /// </param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<LinkResponseData> CreateLinkAsync(string originalUrl)
    {
        var id = await _db.Database.SqlQueryRaw<long>("SELECT nextval('\"LinkSequence\"') AS \"Value\"").FirstAsync();
        var shortUrl = Encoder.GetEncoded(id);
        var link = new Link
        {
            Id = id,
            OriginalUrl = originalUrl,
            ShortUrl = shortUrl,
        };
        _db.Links.Add(link);
        await _db.SaveChangesAsync();
        await _redis.SetStringAsync(shortUrl, JsonSerializer.Serialize(link), CacheOptions);
        return new LinkResponseData { OriginalUrl = link.OriginalUrl, ShortUrl = link.ShortUrl };
    }
}
