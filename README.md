# link-shortener

A simple URL shortener built with ASP.NET Core, PostgreSQL, and Redis.

## How it works

1. POST a URL to `/shorten` and get back a 6-character base62 short code
2. GET `/{code}` to redirect to the original URL
3. Redis caches lookups, PostgreSQL stores everything persistently
4. IDs come from a PostgreSQL sequence, encoded to base62 for compact URLs

## Running it

```bash
docker compose up
```

App runs on `localhost:5000`. Postgres on `5432`, Redis on `6379`.

## API

**Shorten a URL**

```bash
curl -X POST http://localhost:5000/shorten \
  -H "Content-Type: application/json" \
  -d '{"originalUrl": "https://example.com/some/long/path"}'
```

```json
{
  "originalUrl": "https://example.com/some/long/path",
  "shortUrl": "xK9mBa"
}
```

**Redirect**

```
GET http://localhost:5000/xK9mBa → 302 → https://example.com/some/long/path
```

## Stack

- .NET 10 minimal API
- PostgreSQL (storage + sequence-based ID generation)
- Redis (read-through cache with 30min sliding expiration)
- Docker Compose for local dev

## Known shortcomings

This is a fun weekend project, not production-ready. Missing pieces include:

- **No input validation** — no URL format checks, no length limits
- **No error handling** — no try/catch, no global exception handler, raw 500s on failure
- **No rate limiting** — the `/shorten` endpoint is wide open
- **No logging** — no structured logging, no request tracing
- **Predictable short codes** — sequential IDs make URLs enumerable (FPE would fix this)
- **Catch-all route** — `/{url}` swallows favicon, robots.txt, etc.

## License

MIT
