using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;

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
            return _context.StockItems;
        }

        [HttpPost]
        public void Post([FromBody]StockItemDto item)
        {
            // construct a stockitem
            var stock = new StockItem(
                new ProductType(item.TypeId),
                item.Quantity,
                item.Price,
                new StockOwner(item.OwnerId)
            );

            _context.StockItems.Add(stock);
            _context.ProductTypes.Attach(stock.Type);
            _context.StockOwners.Attach(stock.Owner);
            _context.SaveChanges();

            // Announce that inventory has been added
            _messageBroker.Send(stock);
        }
    }
}