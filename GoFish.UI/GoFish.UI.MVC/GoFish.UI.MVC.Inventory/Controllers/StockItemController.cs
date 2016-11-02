using System;
using System.Threading.Tasks;
using GoFish.Shared.Dto;
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
        private readonly IOptions<ApplicationSettings> _options;

        public StockItemController(IOptions<ApplicationSettings> options, IUserDetails userDetails)
        {
            _userDetails = userDetails;
            _options = options;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Summary(Guid id)
        {
            var stock = await GetData($"{_options.Value.InventoryApiUrl}stockitem/{id}");
            var vm = JsonConvert.DeserializeObject<StockItemViewModel>(stock);

            vm.DashboardUrl = _options.Value.DashboardUrl;
            vm.UserName = _userDetails.GetUserName();

            return View(vm);
        }

        [HttpGet]
        [Route("[action]/{id:Guid?}")]
        public IActionResult Add(Guid? id)
        {
            var stock = new StockItemDto();
            if (id.HasValue)
                stock = new StockItemDto(); //await GetAdvert((Guid)advertId);

            var vm = new EditStockItemViewModel()
            {
                UserName = _userDetails.GetUserName(),
                UserId = _userDetails.GetUserId(),
                DashboardUrl = _options.Value.DashboardUrl,
                StockItem = stock
            };

            return View(vm);
        }

        [HttpPost]
        [Route("[action]/{id:Guid?}")]
        public async Task<IActionResult> Add(Guid id, EditStockItemViewModel vm)
        {
            if (vm.SubmitButton == "Save")
            {
                if (!ModelState.IsValid)
                    return View(vm);

                var response = await PutData($"{_options.Value.InventoryApiUrl}stockitem/{vm.StockItem.Id}", GetJsonContent(vm.StockItem)); // TODO: Check return codes etc. for error conditions.
                return RedirectToAction("Summary", "StockItem", new { id = vm.StockItem.Id });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}