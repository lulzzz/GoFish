// using System.Net.Http;
// using System.Threading.Tasks;
// using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

using StackExchange.Redis;

namespace GoFish.UI.MVC
{
    public class HomeController : SecureController
    {
        public IActionResult Index()
        // public async Task<IActionResult> Index()
        {
            // var client = new HttpClient();
            // var bearerToken = await GetBearerToken();
            // client.SetBearerToken(bearerToken);

            // ViewBag.AccessToken = bearerToken;

            // var response = await client.GetAsync("http://localhost:8081/api/postedadverts"); // Advert API
            // // var response = await client.GetAsync("http://172.17.0.1:5000/api/postedadverts"); // Advert API
            // var content = response.Content.ReadAsStringAsync().Result;

            // ViewBag.Content = content;

            // ViewBag.RedisString = REDIS_Test();

            return View();
        }

        public string REDIS_Test()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:8085");
            // ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("172.17.0.1:6379");
            // ConnectionMultiplexer redis = ConnectionMultiplexer.ConnectAsync("gofish.redis:6379").Result;

            IDatabase db = redis.GetDatabase();

            string value = "abcdefg";
            db.StringSet("mykey", value);

            string value2 = db.StringGet("mykey");

            return value2;
        }
    }
}