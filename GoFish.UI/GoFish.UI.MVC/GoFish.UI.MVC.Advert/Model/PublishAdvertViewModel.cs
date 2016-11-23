using System;
using System.ComponentModel.DataAnnotations;

namespace GoFish.UI.MVC.Advert
{
    public class PublishAdvertViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage="You need to select an option", AllowEmptyStrings = false)]
        public string PublishType { get; set; }

        public int? StockQuantity { get; set; }

        public string SubmitButton { get; set; }
    }
}