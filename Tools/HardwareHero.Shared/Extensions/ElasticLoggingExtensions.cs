using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace HardwareHero.Shared.Extensions
{
    public static class ElasticLoggingExtensions
    {
        public static ElasticsearchSinkOptions ConfigureElasticSink(this IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
                NumberOfReplicas = 1,
                NumberOfShards = 2
            };
        }
    }
}
