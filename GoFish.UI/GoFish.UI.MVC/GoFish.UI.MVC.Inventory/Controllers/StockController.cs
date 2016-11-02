using System.Linq;
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
        private readonly IOptions<ApplicationSettings> _options;


        public StockController(IOptions<ApplicationSettings> options, IUserDetails userDetails)
        {
            _userDetails = userDetails;
            _options = options;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Shelf(int id)
        {
            var stock = await GetData($"{_options.Value.InventoryApiUrl}shelves/{id}");
            var vm = JsonConvert.DeserializeObject<ShelfViewModel>(stock);

            if (!vm.ShelfHasBatches) // Shortcut
                return RedirectToAction("Summary", "StockItem", new { id = vm.StockItems.First().Id });

            vm.DashboardUrl = _options.Value.DashboardUrl;
            vm.UserName = _userDetails.GetUserName();

            return View(vm);
        }
    }
}