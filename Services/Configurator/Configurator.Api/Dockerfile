FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Configurator/Configurator.Api/Configurator.Api.csproj", "Services/Configurator/Configurator.Api/"]
COPY ["Services/HardwareHero.Services.Shared/HardwareHero.Services.Shared.csproj", "Services/HardwareHero.Services.Shared/"]
COPY ["Services/Configurator/Configurator.BusinessLogic/Configurator.BusinessLogic.csproj", "Services/Configurator/Configurator.BusinessLogic/"]
RUN dotnet restore "Services/Configurator/Configurator.Api/Configurator.Api.csproj"
COPY . .
WORKDIR "/src/Services/Configurator/Configurator.Api"
RUN dotnet build "Configurator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Configurator.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Configurator.Api.dll"]