using LinkShortener.DTOs;
using LinkShortener.Postgres;
using LinkShortener.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PgDB>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "LinkShortener:";
});
builder.Services.AddScoped<LinkService>();
builder.Services.AddSingleton<Counter>();

var app = builder.Build();

app.MapGet(
    "/{url}",
    async (LinkService linkService, string url) =>
    {
        var link = await linkService.GetLinkAsync(url);
        return link != null ? Results.Ok(link) : Results.NotFound("Short URL not found");
    }
);

app.MapGet("/health", () => Results.Ok("Service is healthy"));

app.MapGet(
    "/redirect/{url}",
    async (LinkService linkService, string url) =>
    {
        var link = await linkService.GetLinkAsync(url);
        return link != null ? Results.Redirect(link.OriginalUrl) : Results.NotFound("Short URL not found");
    }
);

app.MapPost(
    "/shorten",
    async ([FromBody] LinkRequestData linkDto, LinkService service) =>
    {
        var link = await service.CreateLinkAsync(linkDto.OriginalUrl);
        return Results.Ok(link);
    }
);

await app.RunAsync();
