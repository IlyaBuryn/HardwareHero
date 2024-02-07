using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareHero.Shared.Helpers
{
    public static class ScriptHelper
    {
        public static async Task InitDatabaseWithSqlScript<Context>
            (IApplicationBuilder app, string scriptPath) where Context : DbContext
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<Context>();
                    context!.Database.Migrate();

                    var scriptFiles = Directory.GetFiles(scriptPath, "*.sql");

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
