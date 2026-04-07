using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Postgres;

public class PgDB(DbContextOptions<PgDB> options) : DbContext(options)
{
    public required DbSet<Link> Links { get; set; }
}
