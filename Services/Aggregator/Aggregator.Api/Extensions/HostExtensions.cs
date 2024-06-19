using HardwareHero.Shared.Extensions;
using Serilog;
using Serilog.Exceptions;

namespace Aggregator.Api.Extensions
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

    //private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
    //{
    //    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    //    {
    //        AutoRegisterTemplate = true,
    //        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
    //        NumberOfReplicas = 1,
    //        NumberOfShards = 2
    //    };
    //}
}
