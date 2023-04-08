using ConsoleTestApp;
using HardwareHero.Services.Shared.Clients.IdentityServer;
using HardwareHero.Services.Shared.Clients.UserManagementService;
using HardwareHero.Services.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient<IdentityServerClient>();
                services.AddHttpClient<UserClient>();
                services.AddHttpClient<RoleClient>();


                services.AddTransient<AuthenticationServiceTest>();

                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false);

                IConfiguration configuration = configurationBuilder.Build();

                services.Configure<IdentityServerApiOptions>
                    (configuration.GetSection(IdentityServerApiOptions.SectionName));

                services.Configure<ServiceAddressOptions>
                    (configuration.GetSection(ServiceAddressOptions.SectionName));

            })
            .ConfigureLogging(logging =>
            {
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .UseConsoleLifetime();

        var host = builder.Build();

        using (var serviceScope = host.Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;

            try
            {
                var service = services.GetRequiredService<AuthenticationServiceTest>();

                var rolesResult = await service.RunRolesClientTestsAsync(args);
                var usersResult = await service.RunUsersClientTestsAsync(args);

                Console.WriteLine(rolesResult);
                Console.WriteLine(usersResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        Console.ReadKey();

        return 0;   
    }
}