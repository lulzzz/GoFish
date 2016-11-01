using System;

namespace GoFish.UI.MVC.Inventory
{
    public class StockItemViewModel : UserOwnedViewModel
    {
        public Guid Id { get; set; }
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}