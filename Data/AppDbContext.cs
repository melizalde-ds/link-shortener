using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<Link> Links { get; set; }
}
