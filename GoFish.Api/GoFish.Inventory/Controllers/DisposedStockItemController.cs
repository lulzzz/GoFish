using Microsoft.AspNetCore.Mvc;
using GoFish.Shared.Interface;
using System;
using System.Net;

namespace GoFish.Inventory
{
    [Route("api/[controller]")]
    public class DisposedStockItemController : InventoryController
    {
        private readonly ICommandMediator _commandMediator;

        public DisposedStockItemController(ICommandMediator commandMediator)
        {
            _commandMediator = commandMediator;
        }

        [HttpPut("{id:Guid}")]
        public IActionResult Put(Guid id)
        {
            try
            {
                _commandMediator.Send(new StockDisposedCommand(id, GetUserId()));
                return new StatusCodeResult((int)HttpStatusCode.Accepted);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}