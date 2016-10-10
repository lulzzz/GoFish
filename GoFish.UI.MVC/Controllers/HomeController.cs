using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GoFish.Shared.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GoFish.UI.MVC
{
    [Route("/")]
    public class HomeController : SecureController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            client.SetBearerToken(await GetBearerToken());

            var response = await client.GetAsync($"http://54.171.92.206:5000/api/adverts?Status=Active");
            var content = response.Content.ReadAsStringAsync().Result;

            return View(new HomeViewModel { ActiveAdverts = JsonConvert.DeserializeObject<List<AdvertDto>>(content) });
        }

        private string REDIS_Test()
        {
            // REDIS for the Read Model - still to do.  This connection works as P.O.C only for now

            // ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:8085");
            // ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("172.17.0.1:6379");
            // ConnectionMultiplexer redis = ConnectionMultiplexer.ConnectAsync("gofish.redis:6379").Result;
            // IDatabase db = redis.GetDatabase();
            // string value = "abcdefg";
            // db.StringSet("mykey", value);
            // string value2 = db.StringGet("mykey");
            // return value2;

            return string.Empty;
        }
    }
}