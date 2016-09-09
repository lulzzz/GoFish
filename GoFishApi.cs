using System.Collections.Generic;
using System.Linq;

namespace GoFish
{
    public class GoFish
    {
        private readonly GoFishContext _context;

        public GoFish (GoFishContext context)
        {
            _context = context;
            AddCatchTypes();
        }

        private void AddCatchTypes()
        {
            if(_context.CatchTypes.Count() == 0)
            {
                _context.CatchTypes.Add(new CatchType(1, "Lobster"));
                _context.CatchTypes.Add(new CatchType(2, "Cod"));
                _context.CatchTypes.Add(new CatchType(3, "Halibut"));
                _context.SaveChanges();
            }
        }

        public void Advertise(Catch advert)
        {
            _context.Catches.Add(advert);
            _context.CatchTypes.Attach(advert.Type);
            _context.SaveChanges();
        }

        public IEnumerable<StockItem> GetStock()
        {
            // Separate this into catches and stock?  CQRS:  Command = Catch, Query = Stock
            var s = _context.Catches.GroupBy(n => n.Type.Name).Select(t => t.Key);

            return from stock in s
                select new StockItem(stock);
        }
    }
}