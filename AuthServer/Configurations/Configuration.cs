using Dgm.Common.Models;
using IdentityServer4;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace AuthServer.Configurations
{
    public class Configuration
    {
        public static IEnumerable<IdentityServer4.EntityFramework.Entities.IdentityResource> GetIdentityResources()
        {
            return new List<IdentityServer4.EntityFramework.Entities.IdentityResource>
            {
                new IdentityResources.OpenId().ToEntity(),
                new IdentityResources.Email().ToEntity(),
                new IdentityResources.Profile().ToEntity()
            };
        }

        public static IEnumerable<IdentityServer4.EntityFramework.Entities.ApiScope> GetApiScopes()
        {
            return new List<IdentityServer4.EntityFramework.Entities.ApiScope>
            {
                new ApiScope(name: "api.read",   displayName: "Read your data.").ToEntity(),
                new ApiScope(name: "api.write",  displayName: "Write your data.").ToEntity(),
                new ApiScope(name: "api.delete", displayName: "Delete your data.").ToEntity()
            };
        }

        public static IEnumerable<IdentityServer4.EntityFramework.Entities.ApiResource> GetApiResources()
        {
            return new List<IdentityServer4.EntityFramework.Entities.ApiResource>
            {
                new ApiResource("resourceapi", "Resource API")
                {
                    Scopes = {"api.read"}
                }.ToEntity(),
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName).ToEntity(),
            };
        }

        public static IEnumerable<IdentityServer4.EntityFramework.Entities.Client> GetClients(IOptions<ClientBaseUrls> clientUrls)
        {
            return new List<IdentityServer4.EntityFramework.Entities.Client> {
                new Client
            {
                RequireConsent = false,
                ClientId = "angular_spa",
                ClientName = "Angular SPA",
                AllowedGrantTypes = GrantTypes.Implicit, //flow of access_token request
                AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api.read"
                    },
                RedirectUris = { $"{clientUrls.Value.ClientApp}auth-callback" },
                PostLogoutRedirectUris = { $"{clientUrls.Value.ClientApp}signout-callback-oidc" },
                AllowedCorsOrigins = { clientUrls.Value.ClientApp },
                AllowAccessTokensViaBrowser = true,
                AccessTokenLifetime = 3600,
                ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                AccessTokenType = AccessTokenType.Jwt,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true
            }.ToEntity(),
                new Client
            {
                ClientId = "demo_api_swagger",
                ClientName = "Swagger UI for demo_api",
                ClientSecrets = { new Secret("secret".Sha256()) }, // change me!
                AllowedGrantTypes = GrantTypes.Implicit,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowAccessTokensViaBrowser = true,
                AccessTokenLifetime = 3600,
                RedirectUris = { $"{clientUrls.Value.AuthServer}/swagger/oauth2-redirect.html", $"{clientUrls.Value.ResourceAPI}swagger/oauth2-redirect.html" },
                AllowedCorsOrigins = { clientUrls.Value.AuthServer, clientUrls.Value.ResourceAPI },
                AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api.read"
                    },
            }.ToEntity(),
                new Client
            {
                ClientId = "flutter",
                RedirectUris = { clientUrls.Value.AuthServer },
                AllowedCorsOrigins = { clientUrls.Value.AuthServer },
                AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api.read"
                    },
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AccessTokenLifetime = 3600
            }.ToEntity()
             };
        }
    }
}
