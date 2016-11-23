using System.ComponentModel.DataAnnotations;

namespace GoFish.UI.MVC.Advert
{
    public class PrePublishAdvertViewModel : AdvertViewModel
    {
        [Required(ErrorMessage = "You need to select an option", AllowEmptyStrings = false)]
        public string PublishType { get; set; }

        [Required]
        public int? StockQuantity { get; set; }
    }
}