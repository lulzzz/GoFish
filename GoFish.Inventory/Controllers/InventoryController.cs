using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace GoFish.Inventory
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly InventoryDbContext _context;
        private readonly IMessageBroker<StockItem> _messageBroker;

        public InventoryController(InventoryDbContext context, IMessageBroker<StockItem> messageBroker)
        {
            _context = context;
            _messageBroker = messageBroker;
        }

        [HttpGet]
        public IEnumerable<StockItem> Get()
        {
            return _context.StockItems
                .Include(pt => pt.ProductType)
                .Include(so => so.Owner);
        }

        [HttpPost]
        public void Post([FromBody]StockItemDto item)
        {
            var stockItem = new StockItem(
                new ProductType(item.ProductTypeId),
                item.Quantity,
                item.Price,
                new StockOwner(item.OwnerId),
                item.AdvertId
            );

            _context.StockItems.Add(stockItem);
            _context.ProductTypes.Attach(stockItem.ProductType);
            _context.StockOwners.Attach(stockItem.Owner);
            _context.SaveChanges();

            // Announce that inventory has been added
            _messageBroker.Send(stockItem);
        }
    }
}