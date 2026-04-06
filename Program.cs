using LinkShortener.Postgres;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PgDB>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddSingleton<IConnectionMultiplexer>(
    await ConnectionMultiplexer.ConnectAsync(
        builder.Configuration.GetConnectionString("RedisConnection")
            ?? throw new MissingFieldException("Redis connection string is missing in configuration.")
    )
);

var app = builder.Build();

app.MapGet("/", async (PgDB db) => await db.Links.ToListAsync());

await app.RunAsync();
