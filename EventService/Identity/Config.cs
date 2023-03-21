using IdentityServer4;
using IdentityServer4.Models;

namespace EventService.Identity;

/// <summary>
/// Конфигурация для Identity Server
/// </summary>
public static class Config
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
            // ReSharper disable once StringLiteralTypo решарпер хочет myApi
            new ApiScope(name: "myapi.access",   displayName: "Access API Backend")
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            // ReSharper disable once StringLiteralTypo решарпер хочет myApi
            new("myapi", "My API")
            {
                Scopes = new List<string>
                {
                    // ReSharper disable once StringLiteralTypo решарпер хочет myApi
                    "myapi.access"
                },
                // ReSharper disable once StringLiteralTypo решарпер хочет hardToGuess
                ApiSecrets = { new Secret("hardtoguess".Sha256()) }
            }
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new()
            {
                ClientId = "spaWeb",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    // ReSharper disable once StringLiteralTypo решарпер хочет hardToGuess
                    new Secret("hardtoguess".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    // ReSharper disable once StringLiteralTypo решарпер хочет myApi
                    "myapi.access"
                },
                AllowOfflineAccess = true, // this to allow SPA,
                AlwaysSendClientClaims = true,
                AlwaysIncludeUserClaimsInIdToken = true

            }
        };
    }
}