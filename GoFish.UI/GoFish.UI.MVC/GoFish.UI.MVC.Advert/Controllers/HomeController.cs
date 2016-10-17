using System.Collections.Generic;
using System.Threading.Tasks;
using GoFish.Shared.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GoFish.UI.MVC.Advert
{
    [Route("/")]
    public class HomeController : SecureController
    {
        public HomeController(IOptions<ApplicationSettings> options) : base(options) { }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var content = await GetData("adverts?Status=Active");

            var vm = new HomeViewModel()
            {
                ActiveAdverts = JsonConvert.DeserializeObject<List<AdvertDto>>(content),
                UserName = GetUserName(),
                DashboardUrl = Options.Value.DashboardUrl
            };

            return View(vm);
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

        [HttpGet]
        [Route("[action]")]
        public async Task Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            await HttpContext.Authentication.SignOutAsync("oidc");
        }
    }
}