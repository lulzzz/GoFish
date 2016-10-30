using System;

namespace GoFish.UI.MVC.Inventory
{
    public class StockViewModel : UserOwnedViewModel
    {
        public ProductType ProductType { get; set; }

        public StockItemViewModel StockItem { get; set; }

    }

    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StockItemViewModel
    {
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}