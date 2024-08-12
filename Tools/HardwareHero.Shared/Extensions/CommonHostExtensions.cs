using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HardwareHero.Shared.Extensions
{
    public static class CommonHostExtensions
    {
        public static void ConfigureSecretsFile(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
             {
                 config.AddJsonFile("/src/.credentials/sharedsettings.json", optional: true, reloadOnChange: true);
             });
        }
    }
}
