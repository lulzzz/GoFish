using System.Collections.Generic;
using System.Threading.Tasks;
using GoFish.Shared.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using GoFish.UI.MVC.Shared;

namespace GoFish.UI.MVC.Advert
{
    [Route("/")]
    public class HomeController : SecureApiController
    {
        private readonly IUserDetails _userDetails;

        public HomeController(IOptions<ApplicationSettings> options, IUserDetails userDetails) : base(options)
        {
            _userDetails = userDetails;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var content = await GetData("adverts?Status=Active");

            var vm = new HomeViewModel()
            {
                ActiveAdverts = JsonConvert.DeserializeObject<List<AdvertDto>>(content),
                UserName = _userDetails.GetUserName(),
                DashboardUrl = Options.Value.DashboardUrl
            };

            return View(vm);
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