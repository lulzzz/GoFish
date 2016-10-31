using System;
using System.Threading.Tasks;
using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

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


        private static StockItemViewModel ParseJsonToDto(string content)
        {
            var jsonObject = JObject.Parse(content);
            StockItemViewModel stock = jsonObject.ToObject<StockItemViewModel>();
            return stock;
        }

        public async Task<IActionResult> Summary(int id)
        {
            var stock = await GetData($"inventory/{id}");
            var stockItem = ParseJsonToDto(stock);

            var vm = new StockViewModel()
            {
                DashboardUrl = _options.Value.DashboardUrl,
                UserName = _userDetails.GetUserName(),
                //
                StockItem = stockItem,
                ProductType = stockItem.ProductType
            };

            return View(vm);
        }
    }
}