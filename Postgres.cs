using Microsoft.EntityFrameworkCore;

using LinkShortener.Models;

namespace LinkShortener.Postgres;

public class PostgresDB: DbContext
{
    public DbSet<Link> Links { get; set; }
    public PostgresDB(DbContextOptions<PostgresDB> options) : base(options){
    }
}