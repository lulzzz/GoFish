using System.Collections.Generic;
using GoFish.Shared.Dto;
using GoFish.UI.MVC.Shared;

namespace GoFish.UI.MVC.Advert
{
    public class HomeViewModel : UserOwnedViewModel
    {
        public IEnumerable<AdvertDto> ActiveAdverts { get; set; }
    }
}