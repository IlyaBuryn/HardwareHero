FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Prices/Prices.Api/Prices.Api.csproj", "Services/Prices/Prices.Api/"]
COPY ["Services/HardwareHero.Services.Shared/HardwareHero.Services.Shared.csproj", "Services/HardwareHero.Services.Shared/"]
COPY ["Services/Prices/Prices.BusinessLogic/Prices.BusinessLogic.csproj", "Services/Prices/Prices.BusinessLogic/"]
RUN dotnet restore "Services/Prices/Prices.Api/Prices.Api.csproj"
COPY . .
WORKDIR "/src/Services/Prices/Prices.Api"
RUN dotnet build "Prices.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Prices.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prices.Api.dll"]