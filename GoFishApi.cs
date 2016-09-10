using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GoFish
{
    public class GoFish
    {
        private readonly GoFishContext _context;

        public GoFish (GoFishContext context)
        {
            _context = context;
            AddCatchTypes();
            AddDudes();
        }

        private void AddCatchTypes()
        {
            if(_context.ProductTypes.Count() == 0)
            {
                _context.ProductTypes.Add(new ProductType(1, "Lobster"));
                _context.ProductTypes.Add(new ProductType(2, "Cod"));
                _context.ProductTypes.Add(new ProductType(3, "Halibut"));
                _context.SaveChanges();
            }
        }

        private void AddDudes()
        {
            if(_context.Dudes.Count() == 0)
            {
                _context.Dudes.Add(new Dude(1, "Henry"));
                _context.Dudes.Add(new Dude(2, "Fiona"));
                _context.Dudes.Add(new Dude(3, "Marvin"));
                _context.SaveChanges();
            }
        }

        internal void Buy(ProductType catchType)
        {
            var stock = _context.StockItems.Where(ct => ct.Type == catchType && ct.Quantity > 0).FirstOrDefault();
            stock.Decrease();
            _context.Update(stock);
            _context.SaveChanges();
        }

        public void Advertise(Catch catchToAdvertize)
        {
            _context.Catches.Add(catchToAdvertize);

            _context.Dudes.Attach(catchToAdvertize.CaughtBy);
            _context.ProductTypes.Attach(catchToAdvertize.Type);

            _context.StockItems.Add(
                new StockItem(
                    catchToAdvertize.Type,
                    catchToAdvertize.Quantity,
                    catchToAdvertize.Price,
                    catchToAdvertize.CaughtBy
                )
            );

            _context.SaveChanges();
        }

        public IEnumerable<StockItem> GetStock()
        {
            return _context
                .StockItems
                .Where(q => q.Quantity > 0)
                .Include(t => t.Type)
                .Include(d => d.Seller);
        }
    }
}