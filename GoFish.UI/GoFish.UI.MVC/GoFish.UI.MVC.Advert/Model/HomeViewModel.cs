using System.Collections.Generic;
using GoFish.Shared.Dto;

namespace GoFish.UI.MVC.Advert
{
    public class HomeViewModel : UserOwnedViewModel
    {
        public IEnumerable<AdvertDto> ActiveAdverts { get; set; }
    }
}