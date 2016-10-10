using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.UI.MVC
{
    public class SecureController : Controller
    {
        public async Task<string> GetBearerToken()
        {
            var disco = await DiscoveryClient.GetAsync("http://54.171.92.206:5002"); // Identity Server API
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
            return tokenResponse.AccessToken;
        }
    }
}