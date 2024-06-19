using HardwareHero.Shared.Extensions;
using Serilog;
using Serilog.Exceptions;

namespace Configurator.Api.Extensions
{
    public static class HostExtensions
    {
        public static void ConfigureElasticLogging(this IHostBuilder host)
        {
            var environment = Environment
                .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(".logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Elasticsearch(configuration.ConfigureElasticSink(environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            host.UseSerilog();
        }
    }
}
