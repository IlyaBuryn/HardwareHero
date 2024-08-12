namespace ApiGateway.Ocelot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureCORSPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}
