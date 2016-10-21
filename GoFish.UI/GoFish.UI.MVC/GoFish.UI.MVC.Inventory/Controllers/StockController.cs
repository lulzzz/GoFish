using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoFish.UI.MVC.Inventory
{
    public class StockController : SecureApiController
    {
        private readonly IOptions<ApplicationSettings> _options;
        private readonly IUserDetails _userDetails;

        public StockController(IOptions<ApplicationSettings> options, IUserDetails userDetails) : base(options)
        {
            _options = options;
            _userDetails = userDetails;
        }

        public IActionResult Summary(int productTypeId)
        {
            var vm = new StockViewModel()
            {
                DashboardUrl = _options.Value.DashboardUrl,
                UserName = _userDetails.GetUserName(),
                //
                ProductName = "Lobster"
            };

            return View(vm);
        }
    }
}