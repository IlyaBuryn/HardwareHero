using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace HardwareHero.Shared.Extensions
{
    public static class CommonServiceCollectionExtensions
    {
        public static void ConfigureCommonOpenTelemetry(
            this IServiceCollection services,
            string OpenRemoteManageMeterName,
            string meter, // builder.Configuration.GetValue<string>("OpenRemoteManageMeterName")
            string endpoint) // builder.Configuration["Otel:Endpoint"]
        {
            services.AddOpenTelemetry()
                .WithMetrics(opt =>
                    opt
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService($"{OpenRemoteManageMeterName}.GatewayAPI"))
                        .AddMeter(meter)
                        .AddAspNetCoreInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddProcessInstrumentation()
                        .AddOtlpExporter(opts =>
                        {
                            opts.Endpoint = new Uri(endpoint);
                        })
                );
        }
    }
}
