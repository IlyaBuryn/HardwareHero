using Duende.IdentityServer.Models;
using HardwareHero.Services.Shared.Constants;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public record IdentitySeedUser(string Id, string UserName, string Name, string Email, DateTime? RegistrationDate, string? Password, string[] Roles, bool EmailConfirmed = true);
    public record IdentitySeedRole(string Name, string NormalizedName);

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "Your role(s)", new []{ JwtClaimTypes.Role }),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(IdentityClientConstants.ServicesApiScope),
            new ApiScope(IdentityClientConstants.WebScope),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "test.client",
                ClientName = "Test client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes =
                {
                    IdentityClientConstants.ServicesApiScope
                },
            },
            new Client
            {
                ClientId = "react.web.app",
                ClientName = "spa",
                ClientSecrets = {new Secret("secret".Sha256())},
                RequireClientSecret = false,
                AllowedGrantTypes =  new[] { GrantType.AuthorizationCode, GrantType.ResourceOwnerPassword },

                AllowOfflineAccess = true,

                RedirectUris = { "http://localhost:5001/home" },
                PostLogoutRedirectUris = { "http://localhost:5001/home" },
                AllowedCorsOrigins= { "http://localhost:5001" },

                AllowedScopes = new List<string>
                {
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.Email,
                    IdentityClientConstants.WebScope,
                    IdentityClientConstants.ServicesApiScope,
                    "roles",
                },
            }
        };

    public static IEnumerable<IdentitySeedRole> IdentitySeedRoles =>
        new[]
        {
            new IdentitySeedRole(Name: "User", NormalizedName: "User"),
            new IdentitySeedRole(Name: "Admin", NormalizedName: "Administrator"),
            new IdentitySeedRole(Name: "Manager", NormalizedName: "Manager"),
            new IdentitySeedRole(Name: "Contributor", NormalizedName: "Contributor"),

        };

    public static IEnumerable<IdentitySeedUser> IdentitySeedUsers =>
        new[]
        {
            // Admin
            new IdentitySeedUser(Id: "9fe09964-898b-4ece-a0da-58dd32cb90d0", UserName: "admin", Name: "Admin", Email: "admin@m.com", null, null, Roles: new[] { "Admin", "Manager", "Contributor", "User" }),

            // Manager
            new IdentitySeedUser(Id: "f2a66e51-5af7-40b2-a38d-4157d3d1abb1", UserName: "isaac", Name: "Isaac Bishop", Email: "isaac@m.com", null, null, Roles: new[] { "Manager", "Contributor", "User" }),

            // Just users
            new IdentitySeedUser(Id: "6b3ba2d5-9489-48dc-a294-40ea688235fb", UserName: "ivan", Name: "Ivan Ivanovich", Email: "ivan@m.com", null, null, Roles: new[] { "User" }),
            new IdentitySeedUser(Id: "a02401fa-5172-43bd-9d38-2dbf220474b5", UserName: "jacob9090", Name: "Jacob Rodriges", Email: "jr.forever@example.org", null, null, Roles: new[] { "User" }),
            new IdentitySeedUser(Id: "f5fa4ef9-978a-40a4-a8a7-9baab1fb835e", UserName: "furyricky", Name: "Ricky Fury", Email: "rf.rf.rf@example.com", null, null, Roles: new[] { "User" }),

            // Contributors [by]
            new IdentitySeedUser(Id: "a9caa7b2-109b-4c21-bc24-749ff87b9b18", UserName: "johndoe", Name: "John Doe", Email: "johndoe@example.com", null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "274f801d-2117-48ff-96f7-ecb9b193bc7f", UserName: "alicesmith", Name: "Alice Smith", Email: "alicesmith@example.com" , null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "46f5064b-a6fe-4b58-b303-9ed344700195", UserName: "mikejohnson", Name: "Mike Johnson", Email: "mikejohnson@example.com", null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "7c5086ea-4faf-4db2-91a4-c1217a2f3029", UserName: "laurawilliams", Name: "Laura Williams", Email: "laurawilliams@example.com" , null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "b0a3bda2-525d-42c0-b7ff-8b0f68b4ca84", UserName: "davidbrown", Name: "David Brown", Email: "davidbrown@example.com", null, null, Roles: new[] { "Contributor", "User" }),

            // Contributors [ru.msk]
            new IdentitySeedUser(Id: "17f87d98-17f0-4708-a7ff-0cb4ec09b58a", UserName: "sarahwilson", Name: "Sarah Wilson", Email: "sarahwilson@example.com", null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "8bc0e747-443a-4f62-a05a-6e7d8cb1516f", UserName: "peterjackson", Name: "Peter Jackson", Email: "peterjackson@example.com", null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "02e91bcd-c2f5-4025-8f2f-5bac70b6924c", UserName: "emilythompson", Name: "Emily Thompson", Email: "emilythompson@example.com", null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "ffcd6b86-9327-4b7a-b2ad-ec13cf531d3f", UserName: "robertrodriguez", Name: "Robert Rodriguez", Email: "robertrodriguez@example.com", null, null, Roles: new[] { "Contributor", "User" }),

            // Contributors [pl]
            new IdentitySeedUser(Id: "67bfe5a9-28e2-4c55-8549-888556d2a670", UserName: "jessicamiller", Name: "Jessica Miller", Email: "jessicamiller@example.com", null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "34302079-5037-499e-8703-9920be62adf7", UserName: "ryanjackson", Name: "Ryan Jackson", Email: "ryanjackson@example.com", null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "0a3a8a9f-9bb3-4e06-a050-20c953855795", UserName: "oliviamartin", Name: "Olivia Martin", Email: "oliviamartin@example.com", null, null, Roles: new[] { "Contributor", "User" }),

            // Contributors [ru.spb]
            new IdentitySeedUser(Id: "bad8a170-deb8-44e7-965a-2f660079d5ed", UserName: "sophiawalker", Name: "Sophia Walker", Email: "sophiawalker@example.com", null, null, Roles: new[] { "Contributor", "User" }),
            new IdentitySeedUser(Id: "373ec651-0d88-45f4-90dd-4b2a98500ecb", UserName: "danielharris", Name: "Daniel Harris", Email: "danielharris@example.com", null, null, Roles: new[] { "Contributor", "User" }),
        };
}