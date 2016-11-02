using System.Collections.Generic;
using System.Threading.Tasks;
using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GoFish.UI.MVC.Dashboard
{
    [Authorize]
    [Route("/")]
    public class HomeController : SecureApiController
    {
        private readonly IUserDetails _userDetails;
        private readonly IOptions<ApplicationSettings> _options;

        public HomeController(IUserDetails userdetails, IOptions<ApplicationSettings> options)
        {
            _userDetails = userdetails;
            _options = options;
        }

        public async Task<IActionResult> Index()
        {
            var advertData = await GetData(_options.Value.AdvertApiUrl + "publishedadverts");
            var adverts = JsonConvert.DeserializeObject<List<AdvertViewModel>>(advertData);

            var vm = new HomeViewModel
            {
                UserName = _userDetails.GetUserName(),
                CurrentAdverts = adverts
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