FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 4001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identity/IdentityServer/IdentityServer.csproj", "Services/Identity/IdentityServer/"]
COPY ["Services/HardwareHero.Services.Shared/HardwareHero.Services.Shared.csproj", "Services/HardwareHero.Services.Shared/"]
RUN dotnet restore "Services/Identity/IdentityServer/IdentityServer.csproj"
COPY . .
WORKDIR "/src/Services/Identity/IdentityServer"
RUN dotnet build "IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]