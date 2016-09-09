using System;
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

        internal void Buy(CatchType catchType)
        {
            var stock = _context.StockItems.Where(ct => ct.Name == catchType.Name && ct.Quantity > 0).FirstOrDefault();
            stock.Decrease();
            _context.Update(stock);
            _context.SaveChanges();
        }

        public void Advertise(Catch advert)
        {
            _context.Catches.Add(advert);
            _context.CatchTypes.Attach(advert.Type);

            _context.StockItems.Add(new StockItem(advert.Type.Name, advert.Quantity));

            _context.SaveChanges();
        }

        public IEnumerable<StockItem> GetStock()
        {
            return _context.StockItems.Where(q => q.Quantity > 0);
        }
    }
}