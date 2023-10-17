using Aggregator.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public async static Task DatabaseInitialization(this IApplicationBuilder app)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<AggregatorDbContext>();
                    context!.Database.Migrate();

                    var scriptFiles = Directory.GetFiles("/src/Services/Aggregator/Aggregator.DataAccess/Scripts", "*.sql");

                    foreach (var scriptFile in scriptFiles)
                    {
                        var scriptContent = await File.ReadAllTextAsync(scriptFile);
                        await context.Database.ExecuteSqlRawAsync(scriptContent);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
