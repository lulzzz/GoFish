using System.Collections.Generic;
using System.Linq;

namespace GoFish.Inventory
{
    public class Shelf
    {
        private IEnumerable<StockItem> _stock;

        public Shelf()
        {
            _stock = new List<StockItem>();
        }

        public Shelf(IEnumerable<StockItem> stock)
        {
            _stock = stock;
        }

        public IEnumerable<StockItem> StockItems
        {
            get
            {
                return _stock;
            }
        }

        public string Name
        {
            get
            {
                return _stock.First().ProductType.Name;
            }
        }
    }
}