using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace GoFish.Inventory
{
    [Authorize]
    [Route("api/[controller]")]
    public class ShelvesController : Controller
    {
        private readonly InventoryDbContext _context;

        public ShelvesController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Shelf> Get()
        {
            var allStock = _context.StockItems
                .Include(pt => pt.ProductType)
                .Include(so => so.Owner)
                .Where(o => o.Owner.Id == GetUserId());

            return from stock in allStock
                group stock by stock.ProductType into shelfItems
                select new Shelf(shelfItems.ToList());
        }

        [HttpGet("{id}")]
        public Shelf Get(int id)
        {
            var stock = _context.StockItems
                    .Include(pt => pt.ProductType)
                    .Include(so => so.Owner)
                    .Where(p => p.ProductType.Id == id)
                    .Where(o => o.Owner.Id == GetUserId());

            return new Shelf(stock);
        }


        private int GetUserId()
        {
            return 1; // TODO:  Change this - use a shared library?
        }
    }
}
