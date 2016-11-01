using System;
using System.Threading.Tasks;
using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GoFish.UI.MVC.Inventory
{
    [Route("[controller]")]
    public class StockItemController : SecureApiController
    {
        private readonly IUserDetails _userDetails;

        public StockItemController(IOptions<ApplicationSettings> options, IUserDetails userDetails) : base(options)
        {
            _userDetails = userDetails;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Summary(Guid id)
        {
            var stock = await GetData($"inventory/{id}");
            var vm = JsonConvert.DeserializeObject<StockItemViewModel>(stock);

            vm.DashboardUrl = Options.Value.DashboardUrl;
            vm.UserName = _userDetails.GetUserName();

            return View(vm);
        }
    }
}