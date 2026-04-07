using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<Link> Links { get; set; }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<long>("LinkSequence").StartsAt(0).IncrementsBy(1).HasMin(0).HasMax(long.MaxValue);
    }
}
