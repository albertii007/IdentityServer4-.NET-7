using IdentityServer4.Models;
using System.Collections.Generic;

namespace ASP.IdentityServer.Core.Persistence.Seeds
{
    public class IdentityConfig
    {
        public static IList<IdentityResource> IdentityResources =>
             new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResource(
                        name: "profile",
                        userClaims: new[] { "name", "email", "username" },
                        displayName: "User profile data")
                };

        public static IList<Client> Clients =>
            new List<Client> {
                new Client{
                    ClientId = "IdServer4_",
                    ClientName = "My Project",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {
                        new Secret("identity_secret".Sha256())
                    },
                    AllowedScopes = {
                        "profile",
                        "openid",
                        "accounts.read",
                        "accounts.write",
                        "accounts.delete",
                        "api_account"
                    },
                    AccessTokenLifetime = 10800,
                    AllowOfflineAccess = true,
                    BackChannelLogoutSessionRequired = true,
                    FrontChannelLogoutSessionRequired = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                    AllowAccessTokensViaBrowser = true,
                },
            };

        public static IList<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(name: "accounts.read",   displayName: "Read your data."),
                new ApiScope(name: "accounts.write",  displayName: "Write your data."),
                new ApiScope(name: "accounts.delete", displayName: "Delete your data.")
            };

        public static readonly IEnumerable<ApiResource> ApiResources =
            new[]
            {
                new ApiResource("IdServer4_", "Account Service")
                {
                    ApiSecrets = { new Secret("identity_secret".Sha256()) } ,
                    Scopes = { "accounts.read", "accounts.write", "accounts.delete" }
                }
            };
    }
}

