#!/bin/bash
set -e

dotnet restore
dotnet csharpier format .
dotnet roslynator fix link-shortener.csproj
dotnet build -warnaserror
