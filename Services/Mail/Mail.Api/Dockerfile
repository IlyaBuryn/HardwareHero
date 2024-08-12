FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Mail/Mail.Api/Mail.Api.csproj", "Services/Mail/Mail.Api/"]
COPY ["Services/Mail/Mail.BusinessLogic/Mail.BusinessLogic.csproj", "Services/Mail/Mail.BusinessLogic/"]
COPY ["Services/HardwareHero.Services.Shared/HardwareHero.Services.Shared.csproj", "Services/HardwareHero.Services.Shared/"]
RUN dotnet restore "Services/Mail/Mail.Api/Mail.Api.csproj"
COPY . .
WORKDIR "/src/Services/Mail/Mail.Api"
RUN dotnet build "Mail.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mail.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mail.Api.dll"]