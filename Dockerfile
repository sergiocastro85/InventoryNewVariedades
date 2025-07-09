# Usa la imagen oficial de ASP.NET para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usa el SDK de .NET para compilar la app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "PapeleriaInventary.Web/PapeleriaInventary.Web.csproj"
RUN dotnet publish "PapeleriaInventary.Web/PapeleriaInventary.Web.csproj" -c Release -o /app/publish

# Crea la imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PapeleriaInventary.Web.dll"]