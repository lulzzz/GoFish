using System.Collections.Generic;
using System.Linq;

namespace GoFish.UI.MVC.Inventory
{
    public class ShelfViewModel : UserOwnedViewModel
    {
        public int ProductId
        {
            get
            {
                return StockItems.First().ProductType.Id;
            }
        }

        public string ProductName
        {
            get
            {
                return StockItems.First().ProductType.Name;
            }
        }

        public int Quantity
        {
            get
            {
                return StockItems.Sum(q => q.Quantity);
            }
        }

        public double LowestPrice
        {
            get
            {
                return StockItems.Min(s => s.Price);
            }
        }

        public double HighestPrice
        {
            get
            {
                return StockItems.Max(s => s.Price);
            }
        }

        public string PriceRange
        {
            get
            {
                if (LowestPrice == HighestPrice)
                    return LowestPrice.ToString();

                return $"{LowestPrice} to {HighestPrice}";
            }
        }

        public IList<StockItemViewModel> StockItems { get; set; }
    }

    // TODO: Move this to shared and remove catchtype?
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}