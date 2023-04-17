﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using HardwareHero.Services.Shared.Settings;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;
using HardwareHero.Services.Shared.Constants;

namespace IdentityServer
{
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
                new ApiScope(IdentityClientConstants.UsersApiScope),
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
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityClientConstants.ServicesApiScope,
                        IdentityClientConstants.UsersApiScope,
                        IdentityClientConstants.WebScope
                    }
                },
                new Client
                {
                    ClientId = "external",
                    ClientName ="External Client",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    RequireClientSecret = false,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityClientConstants.WebScope
                    }
                },
            };
    }
}