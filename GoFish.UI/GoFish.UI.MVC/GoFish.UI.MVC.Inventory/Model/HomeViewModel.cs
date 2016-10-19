using System.Collections.Generic;
using GoFish.Shared.Dto;

namespace GoFish.UI.MVC.Inventory
{
    public class HomeViewModel : UserOwnedViewModel
    {
        public IEnumerable<StockItemDto> Stock { get; set; }
    }
}