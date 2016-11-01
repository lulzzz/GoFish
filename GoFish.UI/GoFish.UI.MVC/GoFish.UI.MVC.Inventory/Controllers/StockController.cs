using System.Threading.Tasks;
using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GoFish.UI.MVC.Inventory
{
    [Route("[controller]")]
    public class StockController : SecureApiController
    {
        private readonly IUserDetails _userDetails;

        public StockController(IOptions<ApplicationSettings> options, IUserDetails userDetails) : base(options)
        {
            _userDetails = userDetails;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Shelf(int id)
        {
            var stock = await GetData($"shelves/{id}");
            var vm = JsonConvert.DeserializeObject<ShelfViewModel>(stock);

            vm.DashboardUrl = Options.Value.DashboardUrl;
            vm.UserName = _userDetails.GetUserName();

            return View(vm);
        }
    }
}