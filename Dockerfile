FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/RecipeShare.Api/RecipeShare.Api.csproj", "src/RecipeShare.Api/"]
COPY ["src/RecipeShare.Core/RecipeShare.Core.csproj", "src/RecipeShare.Core/"]
COPY ["src/RecipeShare.Infrastructure/RecipeShare.Infrastructure.csproj", "src/RecipeShare.Infrastructure/"]

RUN dotnet restore "src/RecipeShare.Api/RecipeShare.Api.csproj"

COPY . .

WORKDIR "/src/src/RecipeShare.Api"
RUN dotnet build "RecipeShare.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RecipeShare.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

RUN adduser --disabled-password --home /app --gecos '' --shell /bin/bash appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
  CMD curl --fail http://localhost:8080/healthz || exit 1

ENTRYPOINT ["dotnet", "RecipeShare.Api.dll"]