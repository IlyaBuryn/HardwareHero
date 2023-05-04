FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApiGateway/ApiGateway.Ocelot/ApiGateway.Ocelot.csproj", "ApiGateway/ApiGateway.Ocelot/"]
RUN dotnet restore "ApiGateway/ApiGateway.Ocelot/ApiGateway.Ocelot.csproj"
COPY . .
WORKDIR "/src/ApiGateway/ApiGateway.Ocelot"
RUN dotnet build "ApiGateway.Ocelot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.Ocelot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.Ocelot.dll"]