using Duende.IdentityServer.Models;
using HardwareHero.Services.Shared.Constants;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
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
                ClientName = "Test Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes =
                {
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile,
                    IdentityClientConstants.ServicesApiScope,
                    IdentityClientConstants.WebScope,
                }
            },

            new Client
            {
                ClientId = "external",
                ClientName ="External Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                RequireClientSecret = false,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId,
                    Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile,
                    IdentityClientConstants.WebScope,
                }
            },
        };
}
