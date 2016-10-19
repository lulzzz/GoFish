using Microsoft.AspNetCore.Mvc;

namespace GoFish.UI.MVC.Inventory
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var vm = new UserOwnedViewModel()
            {

            };

            return View(vm);
        }
    }
}