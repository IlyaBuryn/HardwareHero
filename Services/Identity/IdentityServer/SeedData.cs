using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HardwareHero.Services.Shared.Data;
using HardwareHero.Services.Shared.Models.Identity;
using HardwareHero.Services.Shared.Models.UserManagementService;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IdentityServer
{
    public class SeedData
    {
        public static void SetupUserContext(ServiceCollection services, string connectionString)
        {
            services.AddLogging();
            services.AddDbContext<UsersDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();
        }

        public static async Task EnsureSeedData(ServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<UsersDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var roles = new List<IdentityRole>() { 
                        new IdentityRole() { Name = "User", NormalizedName = "User" },
                        new IdentityRole() { Name = "Admin", NormalizedName = "Administrator" },
                        new IdentityRole() { Name = "Manager", NormalizedName = "Manager" },
                        new IdentityRole() { Name = "Contributor", NormalizedName = "Contributor" } };
                    foreach (var role in roles)
                    {
                        var result = await roleMgr.CreateAsync(role);

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
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

                        var result = userMgr.CreateAsync(isaac, "Pass_123").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(isaac, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Isaac Bishop"),
                            new Claim(JwtClaimTypes.GivenName, "Isaac"),
                            new Claim(JwtClaimTypes.FamilyName, "Bishop"),
                            new Claim(JwtClaimTypes.WebSite, "http://isaac-bishop.com"),
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

                        foreach (var role in roles)
                        {
                            result = await userMgr.AddToRoleAsync(isaac, role.Name);
                            if (!result.Succeeded)
                            {
                                throw new Exception(result.Errors.First().Description);
                            }
                        }

                        Log.Debug("Isaac has been updated");
                    }
                }
            }
        }
    }
}
