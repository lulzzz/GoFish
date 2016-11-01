using System.Collections.Generic;

namespace GoFish.UI.MVC.Inventory
{
    public class HomeViewModel : UserOwnedViewModel
    {
        public IEnumerable<ShelfViewModel> Shelves { get; set; }
    }
}