using System.Collections.Generic;
using GoFish.Shared.Dto;
using GoFish.UI.MVC.Shared;

namespace GoFish.UI.MVC.Inventory
{
    public class EditStockItemViewModel : UserOwnedViewModel
    {
        public StockItemDto StockItem { get; set; }

        public IDictionary<string, string> ProductTypes
        {
            get
            {
                return new Dictionary<string, string>
                {
                   {"1", "Lobster"},
                   {"2", "Cod"},
                   {"3", "Halibut"}
                };
            }
        }

        public string SubmitButton { get; set; }

        public string DeleteButtonState
        {
            get
            {
                return string.Empty;
            }
        }

        public string GetToolTip(string button)
        {
            return string.Empty;
        }
    }
}