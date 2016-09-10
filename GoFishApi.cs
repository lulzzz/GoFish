using System;
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

        public IEnumerable<Shipment> GetShipments()
        {
            return _context.Shipments.Include(oi => oi.PurchaseOrderItems);
        }

        private void AddDudes()
        {
            if(_context.Dudes.Count() == 0)
            {
                _context.Dudes.Add(new Dude(1, "Henry"));
                _context.Dudes.Add(new Dude(2, "Fiona"));
                _context.Dudes.Add(new Dude(3, "Marvin"));
                _context.Dudes.Add(new Dude(4, "Pat"));
                _context.Dudes.Add(new Dude(5, "Shep"));
                _context.SaveChanges();
            }
        }

        internal void CreateShipment(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
            _context.SaveChanges();
        }

        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            return _context.PurchaseOrders.Include(poi => poi.OrderItems);
        }

        public void Buy(ProductType catchType)
        {
            var po = new PurchaseOrder();
            po.AddOrderItem(new PurchaseOrderItem(sku: 1));
            _context.PurchaseOrders.Add(po);

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