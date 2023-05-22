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
            new IdentityResource("roles", "Your role(s)", new []{"role"}),
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

    public static Client GetSpaClient =>
        Clients.Where(x => x.ClientName == "spa").FirstOrDefault();
}
