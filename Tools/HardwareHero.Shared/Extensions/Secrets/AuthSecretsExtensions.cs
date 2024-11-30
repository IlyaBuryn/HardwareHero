using HardwareHero.Shared.Constants.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HardwareHero.Shared.Extensions.Secrets
{
    public static class AuthSecretsExtensions
    {
        public static IHostBuilder ConfigureAuthenticationSecretsFiles(
            this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(
                    AuthSecretsConstants.FilePath, 
                    optional: true, 
                    reloadOnChange: true);
            });

            return host;
        }
    }
}
