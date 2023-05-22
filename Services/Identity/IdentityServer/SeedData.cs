using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using HardwareHero.Services.Shared.Data;
using HardwareHero.Services.Shared.Models.Identity;
using HardwareHero.Services.Shared.Models.UserManagementService;
using IdentityModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer;

public class SeedData
{
    public static async Task EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var configurationContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            configurationContext.Database.Migrate();
            EnsureSeedData(configurationContext);

            var applicationContext = scope.ServiceProvider.GetService<UsersDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            applicationContext.Database.Migrate();
            await EnsureSeedData(applicationContext, userManager, roleManager);
        }
    }

    private static async Task EnsureSeedData(UsersDbContext context, UserManager<ApplicationUser> userMgr, RoleManager<IdentityRole> roleMgr)
    {
        context.Database.Migrate();

        var roles = new List<IdentityRole>() {
                new IdentityRole() { Name = "User", NormalizedName = "User" },
                new IdentityRole() { Name = "Admin", NormalizedName = "Administrator" },
                new IdentityRole() { Name = "Manager", NormalizedName = "Manager" },
                new IdentityRole() { Name = "Contributor", NormalizedName = "Contributor" } };

        foreach (var role in roles)
        {
            var roleCheck = await roleMgr.FindByNameAsync(role.Name);
            if (roleCheck == null)
            {
                var result = await roleMgr.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug($"role: \"{role.Name}\" has been created");
            }
        }

        var isaac = userMgr.FindByNameAsync("isaac").Result;
        if (isaac == null)
        {
            isaac = new ApplicationUser
            {
                UserName = "isaac",
                Name = "Isaac Bishop",
                Email = "isaac@email.com",
                EmailConfirmed = true,
                RegistrationDate = DateTime.Now,
                WishList = new WishList()
                {
                    Components = new[]
                    {
                            new WishListComponents()
                            {
                                ComponentId = new Guid("0712D311-71E5-4C5B-8F80-1B1B08180851"),
                            },
                            new WishListComponents()
                            {
                                ComponentId = new Guid("17BB6742-6611-4865-99F4-222610FB1B88"),
                            },
                        }
                }
            };

            var result = userMgr.CreateAsync(isaac, "Isaac_0").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(isaac, new Claim[]{
                    new Claim(JwtClaimTypes.Name, "Isaac Bishop"),
                    new Claim(JwtClaimTypes.GivenName, "Isaac"),
                    new Claim(JwtClaimTypes.FamilyName, "Bishop"),
                    new Claim(JwtClaimTypes.WebSite, "http://isaac-bishop.com"),
                    new Claim(JwtClaimTypes.Role, "Admin"),
                }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("Isaac has been created");
        }
        else
        {
            Log.Debug("Isaac already exists");

            if (isaac.WishList == null)
            {
                isaac.WishList = new WishList()
                {
                    Components = new[]
                    {
                            new WishListComponents()
                            {
                                ComponentId = new Guid("0712D311-71E5-4C5B-8F80-1B1B08180851"),
                            },
                            new WishListComponents()
                            {
                                ComponentId = new Guid("17BB6742-6611-4865-99F4-222610FB1B88"),
                            },
                        }
                };
            }

            var result = userMgr.UpdateAsync(isaac).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("Isaac has been updated");
        }

        foreach (var role in roles)
        {
            var roleCheck = await userMgr.IsInRoleAsync(isaac, role.Name);
            if (!roleCheck)
            {
                var result = await userMgr.AddToRoleAsync(isaac, role.Name);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug($"Role \"{role.Name}\" added to user: \"{isaac}\"");
            }
        }
    }

    private static void EnsureSeedData(ConfigurationDbContext context)
    {
        if (!context.Clients.Any())
        {
            Log.Debug("Clients being populated");
            foreach (var client in Config.Clients.ToList())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("Clients already populated");
        }

        if (!context.IdentityResources.Any())
        {
            Log.Debug("IdentityResources being populated");
            foreach (var resource in Config.IdentityResources.ToList())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("IdentityResources already populated");
        }

        if (!context.ApiScopes.Any())
        {
            Log.Debug("ApiScopes being populated");
            foreach (var resource in Config.ApiScopes.ToList())
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
        else
        {
            Log.Debug("ApiScopes already populated");
        }

        if (!context.IdentityProviders.Any())
        {
            Log.Debug("OIDC IdentityProviders being populated");
            context.IdentityProviders.Add(new OidcProvider
            {
                Scheme = "demoidsrv",
                DisplayName = "IdentityServer",
                Authority = "https://demo.duendesoftware.com",
                ClientId = "login",
            }.ToEntity());
            context.SaveChanges();
        }
        else
        {
            Log.Debug("OIDC IdentityProviders already populated");
        }
    }
}
