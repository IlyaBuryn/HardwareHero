using Microsoft.Extensions.DependencyInjection;
using Contributor.DataAccess.Extensions;

namespace Contributor.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection builder, string connectionString)
        {
            //ConfigureServices(builder);
            //ConfigureMapProfiles(builder);
            //ConfigureDtoValidators(builder);

            builder.ConfigureDataAccessLayer(connectionString);
        }

        //private static void ConfigureServices(IServiceCollection service)
        //{
        //    service.AddScoped<IComponentService, ComponentService>();
        //    service.AddScoped<IComponentReviewService, ComponentReviewService>();
        //}

        //private static void ConfigureMapProfiles(IServiceCollection service)
        //{
        //    service.AddAutoMapper(cfg =>
        //    {
        //        cfg.AddProfile<AggregatorMapProfile>();
        //    });
        //}

        //private static void ConfigureDtoValidators(IServiceCollection services)
        //{
        //    var assembly = Assembly.Load(new AssemblyName("HardwareHero.Services.Shared"));
        //    services.AddValidatorsFromAssembly(assembly);
        //}
    }
}
