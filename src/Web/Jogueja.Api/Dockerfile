FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Jogueja.sln", "."]

COPY ["src/Web/Jogueja.Api/Jogueja.Api.csproj", "src/Web/Jogueja.Api/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Core/Core.Application/Core.Application.csproj", "src/Core/Core.Application/"]
COPY ["src/Core/Core.Domain/Core.Domain.csproj", "src/Core/Core.Domain/"]
COPY ["src/Core/Core.Shared/Core.Shared.csproj", "src/Core/Core.Shared/"]
COPY ["src/Core/Core.Infrastructure/Core.Infrastructure.csproj", "src/Core/Core.Infrastructure/"]
COPY ["src/Core/Core.Persistence/Core.Persistence.csproj", "src/Core/Core.Persistence/"]
COPY ["src/Core/Core.Endpoints/Core.Endpoints.csproj", "src/Core/Core.Endpoints/"]
COPY ["src/Modules/Players/Players.Application/Players.Application.csproj", "src/Modules/Players/Players.Application/"]
COPY ["src/Modules/Players/Players.Persistence/Players.Persistence.csproj", "src/Modules/Players/Players.Persistence/"]
COPY ["src/Modules/Players/Players.Domain/Players.Domain.csproj", "src/Modules/Players/Players.Domain/"]
COPY ["src/Modules/CourtOwners/CourtOwners.Application/CourtOwners.Application.csproj", "src/Modules/CourtOwners/CourtOwners.Application/"]
COPY ["src/Modules/CourtOwners/CourtOwners.Persistence/CourtOwners.Persistence.csproj", "src/Modules/CourtOwners/CourtOwners.Persistence/"]
COPY ["src/Modules/CourtOwners/CourtOwners.Domain/CourtOwners.Domain.csproj", "src/Modules/CourtOwners/CourtOwners.Domain/"]

RUN dotnet restore "src/Web/Jogueja.Api/Jogueja.Api.csproj"

COPY . .

WORKDIR "/src/src/Web/Jogueja.Api"
RUN dotnet build "Jogueja.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jogueja.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    TZ=America/Sao_Paulo \
    LC_ALL=pt_BR.UTF-8 \
    LANG=pt_BR.UTF-8 \
    ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production

WORKDIR /app
COPY --from=publish /app/publish .

RUN groupadd -g 1001 appgroup && \
    useradd -u 1001 -g appgroup -s /bin/false -M appuser && \
    chown -R appuser:appgroup /app
USER appuser

ENTRYPOINT ["dotnet", "Jogueja.Api.dll"]
