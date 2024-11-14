using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HardwareHero.Shared.Extensions.Scripts
{
    public static class ScriptsExtensions
    {
        /// <summary>
        /// Extension method that generate data from sql scripts to database.
        /// Files must have a .sql extension and start with a prefix in the form of a number
        /// that indicates the priority of the file. 
        /// A smaller number indicates that the file will be processed first.
        /// </summary>
        public static async Task InitDatabaseWithScriptsAsync<TContext>
            (this IApplicationBuilder app, string scriptPath) where TContext : DbContext
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
                    context.Database.Migrate();

                    if (!Directory.Exists(scriptPath))
                    {
                        throw new Exception($"Can't run scripts in {scriptPath}");
                    }

                    var scriptFiles = Directory.GetFiles(scriptPath, "*.sql")
                        .Where(f => Path.GetFileName(f)?.Split('.')[0].All(char.IsDigit) == true)
                        .OrderBy(f => int.Parse(Path.GetFileName(f)?.Split('.')[0]))
                        .ToList();

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
                throw new Exception($"Error when executing scripts: {ex}");
            }
        }
    }
}
