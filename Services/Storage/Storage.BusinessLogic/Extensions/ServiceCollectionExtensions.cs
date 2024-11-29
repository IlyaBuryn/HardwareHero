namespace Storage.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBusinessLayer(
            this IServiceCollection services)
        {
            services
                .ConfigureRepositories()
                .ConfigureServices();

            return services;
        }

        internal static IServiceCollection ConfigureRepositories(
            this IServiceCollection services)
        {
            services
                .AddScoped<IFileRepositoryAsync, FileStorageRepositoryAsync>();

            return services;
        }

        internal static IServiceCollection ConfigureServices(
            this IServiceCollection services)
        {
            services
                .AddScoped<IFileEventService, FileEventService>()
                .AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
