using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.UI.MVC.Dashboard
{
    [Authorize]
    [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var vm = new UserOwnedViewModel { UserName = GetUserName() };
            return View(vm);
        }

        protected string GetUserName()
        {
            var userClaim = HttpContext.User.Claims.Where(u => u.Type == "name").SingleOrDefault();
            return userClaim == null ? "" : userClaim.Value;
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