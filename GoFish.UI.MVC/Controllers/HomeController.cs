using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.UI.MVC
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5005");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/postedadverts");
            var content = response.Content.ReadAsStringAsync().Result;

            ViewBag.AccessToken = tokenResponse.AccessToken;
            ViewBag.Content = content;

            return View();
        }
    }
}