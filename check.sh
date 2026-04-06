#!/bin/bash
set -e

dotnet restore
dotnet csharpier format .
dotnet roslynator fix link-shortner.csproj
dotnet build -warnaserror
