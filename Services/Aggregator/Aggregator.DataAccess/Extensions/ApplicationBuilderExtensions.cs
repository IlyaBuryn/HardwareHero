using Aggregator.DataAccess.Data;
using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aggregator.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public async static Task MigrationInitialization(this IApplicationBuilder app)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<AggregatorDbContext>();
                    context!.Database.Migrate();

                    var filenames = new[] {
                    "addfiles/case.json",
                    "addfiles/cooler.json",
                    "addfiles/cpu.json",
                    "addfiles/gpu.json",
                    "addfiles/mb.json",
                    "addfiles/psu.json",
                    "addfiles/ram.json",
                    "addfiles/sd.json" };

                    string jsonData;
                    foreach (var filename in filenames)
                    {
                        using (StreamReader reader = new StreamReader(filename))
                        {
                            jsonData = reader.ReadToEnd();
                        }

                        JArray data = JsonConvert.DeserializeObject<JArray>(jsonData);

                        foreach (JObject item in data)
                        {
                            if (await context.Components.FirstOrDefaultAsync(x => x.Id == new Guid(item["id"].ToString())) == null)
                            {
                                string id = item["id"].ToString();
                                string name = item["name"].ToString();
                                string description = item["description"].ToString();
                                string price = item["initialPrice"].ToString();
                                string images = item["images"].ToString();
                                string specifications = item["specifications"].ToString();

                                var component = new Component
                                {
                                    Id = new Guid(id),
                                    Name = name,
                                    InitialPrice = decimal.Parse(price),
                                    Description = description,
                                    Specifications = specifications,
                                    Images = images
                                };
                                var result = await context.Components.AddAsync(component);
                            }
                        }
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
