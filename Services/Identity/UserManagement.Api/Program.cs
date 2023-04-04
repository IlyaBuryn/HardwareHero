using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.Data;
using HardwareHero.Services.Shared.Models.UserManagementService;
using HardwareHero.Services.Shared.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionNames.UsersConnection)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<UsersDbContext>()
    .AddDefaultTokenProviders();

//builder.Services
//    .AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "https://localhost:5001";
//        options.RequireHttpsMetadata = false;
//        options.Audience = "https://localhost:5001/resources";
//        //options.TokenValidationParameters.ValidAudiences = new List<string>() { "https://localhost:5001/resources" };
//    });

builder.Services.AddAuthentication(
IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "https://localhost:5001";
        //options.ApiName = "https://localhost:5001/resources";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", IdentityClientSettings.UsersApiScope);
    });
});

builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
