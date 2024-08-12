FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tools/HardwareHero.Shared/HardwareHero.Shared.csproj", "Tools/HardwareHero.Shared/"]
COPY ["Services/Identity/Users.Api/Users.Api.csproj", "Services/Identity/Users.Api/"]
RUN dotnet restore "./Services/Identity/Users.Api/Users.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Users.Api"
RUN dotnet build "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Api.dll"]