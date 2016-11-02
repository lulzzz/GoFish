using System.Collections.Generic;
using GoFish.UI.MVC.Shared;

namespace GoFish.UI.MVC.Dashboard
{
    public class HomeViewModel : UserOwnedViewModel
    {
        public IList<AdvertViewModel> CurrentAdverts { get; set; }
    }
}