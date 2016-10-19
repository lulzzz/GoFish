using System.Collections.Generic;
using System.Threading.Tasks;
using GoFish.Shared.Dto;
using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GoFish.UI.MVC.Inventory
{
    public class HomeController : SecureApiController
    {
        private readonly IOptions<ApplicationSettings> _options;
        private readonly IUserDetails _userDetails;

        public HomeController(IOptions<ApplicationSettings> options, IUserDetails userDetails) : base(options)
        {
            _options = options;
            _userDetails = userDetails;
        }

        public async Task<IActionResult> Index()
        {
            var content = await GetData("inventory");

            var vm = new HomeViewModel()
            {
                DashboardUrl = _options.Value.DashboardUrl,
                UserName = _userDetails.GetUserName(),
                Stock = JsonConvert.DeserializeObject<List<StockItemDto>>(content)
            };

            return View(vm);
        }
    }
}