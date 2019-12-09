using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,

                    RedirectUris = { "https://localhost:5001/signin-oidc"  },
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },
                    ClientSecrets = {new Secret("super-secret".ToSha256(),"mvc-secret") },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "website"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "bruno",
                    Password = "1234",

                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "Bruno"),
                        new Claim(JwtClaimTypes.GivenName, "Bruno Brito"),
                        new Claim(JwtClaimTypes.FamilyName, "Brito"),
                        new Claim(JwtClaimTypes.Email, "bhdebrito@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://www.saindodacaixinha.com.br"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Av Paulista', 'locality': 'Sao Paulo', 'postal_code': 0332303, 'country': 'Brazil' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)

                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "jp",
                    Password = "1234",

                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "JP"),
                        new Claim(JwtClaimTypes.GivenName, "Joao Pedro"),
                        new Claim(JwtClaimTypes.FamilyName, "Silva"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://globo.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Rua da Consolacao', 'locality': 'Sao Paulo', 'postal_code': 02565333, 'country': 'Brasil' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "Brazil")
                    }
                }
            };
        }
    }
}