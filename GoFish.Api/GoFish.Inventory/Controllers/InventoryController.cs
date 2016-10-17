using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;

namespace GoFish.Inventory
{
    [Authorize]
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
        [Authorize("SystemMessagingPolicy")]
        public void Post([FromBody]StockItemDto item)
        {
            var stockItem = new StockItem(
                Guid.NewGuid(),
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
            _messageBroker.SendMessagesFor(stockItem);
        }

        [HttpPut("id:Guid")]
        public IActionResult Put(Guid id, [FromBody]StockItemDto newState)
        {
            // Set the Id from the URI to the DTO to send on
            newState.Id = id;

            // Ensure the current user is the stock owner
            newState.OwnerId = GetUserId();

            try
            {
                // _commandMediator.Send(CreateCommandForState(newState));
                // return Created($"/api/{GetControllerName()}/{id}", _queryMediator.Get(id));
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int GetUserId()
        {
            return 1; // TODO:  Change this - use a shared library?
        }
    }
}