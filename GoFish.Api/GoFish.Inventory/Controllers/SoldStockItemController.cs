using Microsoft.AspNetCore.Mvc;
using GoFish.Shared.Interface;
using System;
using System.Net;
using GoFish.Shared.Dto;

namespace GoFish.Inventory
{
    [Route("api/[controller]")]
    public class SoldStockItemController : InventoryController
    {
        private readonly ICommandMediator _commandMediator;

        public SoldStockItemController(ICommandMediator commandMediator)
        {
            _commandMediator = commandMediator;
        }

        [HttpPut("{id:Guid}")]
        public IActionResult Put(Guid id)
        {
            try
            {
                _commandMediator.Send(new StockSoldCommand(id, GetUserId()));
                return new StatusCodeResult((int)HttpStatusCode.Accepted);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}