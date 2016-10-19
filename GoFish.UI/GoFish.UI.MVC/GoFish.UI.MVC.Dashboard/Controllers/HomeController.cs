using System.Threading.Tasks;
using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.UI.MVC.Dashboard
{
    [Authorize]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IUserDetails _userDetails;

        public HomeController (IUserDetails userdetails)
        {
            _userDetails = userdetails;
        }

        public IActionResult Index()
        {
            var vm = new UserOwnedViewModel { UserName = _userDetails.GetUserName() };
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