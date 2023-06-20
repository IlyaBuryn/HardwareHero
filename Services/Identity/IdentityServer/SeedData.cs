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

        foreach (var role in Config.IdentitySeedRoles)
        {
            var existRoleResult = await roleMgr.FindByNameAsync(role.Name);
            if (existRoleResult == null)
            {
                var result = await roleMgr.CreateAsync(new IdentityRole() { Name = role.Name, NormalizedName = role.NormalizedName });

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug($"role: \"{role.Name}\" has been created!");
            }
        }

        foreach (var user in Config.IdentitySeedUsers)
        {
            var existUserResult = userMgr.FindByNameAsync(user.UserName).Result;
            if (existUserResult == null)
            {
                existUserResult = new ApplicationUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    RegistrationDate = DateTime.Now,
                };

                var createUserResult = userMgr.CreateAsync(existUserResult, "Password_123").Result;
                if (!createUserResult.Succeeded)
                {
                    throw new Exception(createUserResult.Errors.First().Description);
                }

                createUserResult = userMgr.AddClaimsAsync(existUserResult, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, user.Name),
                    new Claim(JwtClaimTypes.Role, user.Roles.First()),
                }).Result;
                if (!createUserResult.Succeeded)
                {
                    throw new Exception(createUserResult.Errors.First().Description);
                }
                Log.Debug($"{user.Name} has been created!");
            }
            else
            {
                Log.Debug($"{user.Name} already exists!");
            }
        }

        foreach (var user in Config.IdentitySeedUsers)
        {
            var applicationUser = userMgr.FindByNameAsync(user.UserName).Result;

            if (applicationUser != null && user.Roles != null && user.Roles.Length != 0)
            {
                var createUserRoleResult = await userMgr.AddToRolesAsync(
                    applicationUser, user.Roles);
                if (!createUserRoleResult.Succeeded)
                {
                    throw new Exception(createUserRoleResult.Errors.First().Description);
                }

                Log.Debug($"Roles added to user: \"{user.UserName}\"!");
            }
            else
            {
                Log.Debug($"Error, when add roles to user \"{user.UserName}\"!");
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
