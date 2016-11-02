using System.Collections.Generic;
using GoFish.UI.MVC.Shared;

namespace GoFish.UI.MVC.Inventory
{
    public class HomeViewModel : UserOwnedViewModel
    {
        public IEnumerable<ShelfViewModel> Shelves { get; set; }
    }
}