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
                    DisplayName = "GoFish",
                    Description = "The GoFish System"
                },
                new Scope
                {
                    Name = "gofish.messaging",
                    DisplayName = "GoFish System Messaging",
                    Description = "System Level Scope For Messaging"
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

                    AllowedScopes = new List<string> { "gofish.messaging" }
                },
                new Client // usual login pair
                {
                    ClientId = "gofish",
                    ClientName = "GoFish client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:8003/signin-oidc",    // Local
                        "http://localhost:5003/signin-oidc",    // Vagrant
                        "http://54.171.92.206:5003/signin-oidc" // Live
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:8003/",    // Local
                        "http://localhost:5003/",    // Vagrant
                        "http://54.171.92.206:5003/" // Live
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
                new InMemoryUser{Subject = "1", Username = "nina", Password = "nina",
                    Claims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Netter Nina"),
                        new Claim(JwtClaimTypes.GivenName, "Nina"),
                        new Claim(JwtClaimTypes.FamilyName, "Netter"),
                        new Claim(JwtClaimTypes.Role, "Fisher")
                    }
                },
                new InMemoryUser{Subject = "2", Username = "fred", Password = "fred",
                    Claims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Fisherman Fred"),
                        new Claim(JwtClaimTypes.GivenName, "Fred"),
                        new Claim(JwtClaimTypes.FamilyName, "Fisherman"),
                        new Claim(JwtClaimTypes.Role, "Fisher")
                    }
                },
            };
        }
    }
}