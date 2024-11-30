using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace HardwareHero.Shared.Extensions.Elastic
{
    public static class ElasticLoggingExtensions
    {
        public static IHostBuilder ConfigureElasticLogging(
            this IHostBuilder host, IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var assembly = Assembly.GetEntryAssembly()?.GetName().Name;

            var logFile = configuration["Elastic:FilePath"] ?? ".logs/log.txt";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("Environment", environment)
                .Enrich.WithProperty("Application", assembly)
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
                .WriteTo.Elasticsearch(configuration.ConfigureElasticSink(assembly, environment))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            host.UseSerilog();
            
            return host;
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(
            this IConfiguration configuration, string? assembly, string? environment)
        {
            var elasticUri = configuration["Elastic:Uri"];
            if (string.IsNullOrEmpty(elasticUri))
            {
                throw new InvalidOperationException("Elasticsearch Uri is not configured");
            }

            return new ElasticsearchSinkOptions(new Uri(elasticUri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{assembly!.ToLower().Replace(".", "-")}-{environment!.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
                NumberOfReplicas = 1,
                NumberOfShards = 2
            };
        }
    }
}
