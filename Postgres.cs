using Microsoft.EntityFrameworkCore;

using LinkShortener.Models;

namespace LinkShortener.Postgres;

public class PgDB: DbContext
{
    public DbSet<Link> Links { get; set; }
    public PgDB(DbContextOptions<PgDB> options) : base(options){
    }
}