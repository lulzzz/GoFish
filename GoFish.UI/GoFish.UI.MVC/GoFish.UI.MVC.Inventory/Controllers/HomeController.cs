using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoFish.UI.MVC.Inventory
{
    public class HomeController : SecureApiController
    {
        private readonly IOptions<ApplicationSettings> _options;
        private readonly IUserDetails _userDetails;

        public HomeController (IOptions<ApplicationSettings> options, IUserDetails userDetails) : base(options)
        {
            _options = options;
            _userDetails = userDetails;
        }

        public IActionResult Index()
        {
            var vm = new UserOwnedViewModel()
            {
                DashboardUrl = _options.Value.DashboardUrl,
                UserName = _userDetails.GetUserName()
            };

            return View(vm);
        }
    }
}