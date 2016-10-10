using System.Collections.Generic;
using GoFish.Shared.Dto;

namespace GoFish.UI.MVC
{
    public class HomeViewModel
    {
        public IEnumerable<AdvertDto> ActiveAdverts { get; set; }
    }
}