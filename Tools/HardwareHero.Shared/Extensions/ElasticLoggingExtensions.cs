using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Elasticsearch;

namespace HardwareHero.Shared.Extensions
{
    public static class ElasticLoggingExtensions
    {
        public static ElasticsearchSinkOptions ConfigureElasticSink(this IConfigurationRoot configuration, string assemblyFrom, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{assemblyFrom.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
                NumberOfReplicas = 1,
                NumberOfShards = 2
            };
        }
    }
}
