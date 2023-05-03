FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Contributor/Contributor.Api/Contributor.Api.csproj", "Services/Contributor/Contributor.Api/"]
COPY ["Services/HardwareHero.Services.Shared/HardwareHero.Services.Shared.csproj", "Services/HardwareHero.Services.Shared/"]
COPY ["Services/Contributor/Contributor.BusinessLogic/Contributor.BusinessLogic.csproj", "Services/Contributor/Contributor.BusinessLogic/"]
COPY ["Services/Contributor/Contributor.DataAccess/Contributor.DataAccess.csproj", "Services/Contributor/Contributor.DataAccess/"]
RUN dotnet restore "Services/Contributor/Contributor.Api/Contributor.Api.csproj"
COPY . .
WORKDIR "/src/Services/Contributor/Contributor.Api"
RUN dotnet build "Contributor.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contributor.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contributor.Api.dll"]