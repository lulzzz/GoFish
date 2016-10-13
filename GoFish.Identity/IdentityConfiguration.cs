using IdentityServer4.Models;
using IdentityServer4.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;

namespace GoFish.Identity
{
    public class IdentityConfiguration
    {
        // scope the parts of the application
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,

                new Scope
                {
                    Name = "api1",
                    DisplayName = "GoFish API",
                    Description = "The GoFish API"
                }
            };
        }

        // clients that want to access scopes
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client // Postman uses client credentials method
                {
                    ClientId = "postman",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                    AllowedScopes = new List<string> { "api1" }
                },
                new Client // MessageQ Clients use client credentials method
                {
                    ClientId = "rabbitmq",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                    AllowedScopes = new List<string> { "api1" }
                },
                new Client // usual login pair
                {
                    ClientId = "gofish",
                    ClientName = "GoFish client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:5003/signin-oidc"
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:5003/"
                    },

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        "api1"
                    }
                }
            };
        }

        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser{Subject = "1", Username = "Beth", Password = "beth",
                    Claims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Beth Buyer"),
                        new Claim(JwtClaimTypes.GivenName, "Beth"),
                        new Claim(JwtClaimTypes.FamilyName, "Buyer"),
                        new Claim(JwtClaimTypes.Role, "Buyer")
                    }
                },
                new InMemoryUser{Subject = "2", Username = "fred", Password = "fred",
                    Claims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Fred Fisherman"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Fisherman"),
                        new Claim(JwtClaimTypes.Role, "Fisherman")
                    }
                },
            };
        }
    }
}