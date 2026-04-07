FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build

WORKDIR /app

COPY *.csproj ./

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime

RUN addgroup -S appgroup && adduser -S appuser -G appgroup

USER appuser

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "link-shortner.dll"]
