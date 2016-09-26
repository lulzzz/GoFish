using IdentityServer4.Models;
using IdentityServer4.Services.InMemory;
using System.Collections.Generic;

namespace GoFish.Identity
{
    public class IdentityConfiguration
    {
        // scope the parts of the application
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                new Scope { Name = "api1", Description = "The GoFish Advert API"}
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
                new Client // Basic resource based Username / Password pair
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                    AllowedScopes = new List<string> { "api1" }
                }
            };
        }

        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "1",
                    Username = "alice",
                    Password = "password"
                },
                new InMemoryUser
                {
                    Subject = "2",
                    Username = "bob",
                    Password = "password2"
                }
            };
        }
    }
}