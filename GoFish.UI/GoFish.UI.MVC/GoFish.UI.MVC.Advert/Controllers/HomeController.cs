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
        private readonly IOptions<ApplicationSettings> _options;

        public HomeController(IOptions<ApplicationSettings> options, IUserDetails userDetails)
        {
            _userDetails = userDetails;
            _options = options;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var content = await GetData($"{_options.Value.AdvertApiUrl}adverts?Status=Active");

            var vm = new HomeViewModel()
            {
                ActiveAdverts = JsonConvert.DeserializeObject<List<AdvertDto>>(content),
                UserName = _userDetails.GetUserName(),
                DashboardUrl = _options.Value.DashboardUrl
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