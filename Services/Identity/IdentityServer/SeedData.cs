// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using HardwareHero.Services.Shared.Data;
using HardwareHero.Services.Shared.Models.Identity;
using HardwareHero.Services.Shared.Models.UserManagementService;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using static Duende.IdentityServer.Models.IdentityResources;

namespace IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<UsersDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<UsersDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
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
                        Log.Debug("Isaac has been updated");
                    }
                }
            }
        }
    }
}
