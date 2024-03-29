using Microsoft.AspNetCore.Mvc;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace GoFish.Inventory
{
    [Route("api/[controller]")]
    public class StockItemController : InventoryController
    {
        private readonly ICommandMediator _commandMediator;

        private readonly InventoryRepository _queryMediator;

        private readonly ILogger<StockItemController> _logger;
        public StockItemController(ICommandMediator commandMediator,
            InventoryRepository repository,
            ILogger<StockItemController> logger)
        {
            _commandMediator = commandMediator;
            _queryMediator = repository;
            _logger = logger;
        }

        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            StockItem stockItem;
            try
            {
                stockItem = _queryMediator.Get(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            if (stockItem == null)
                return NotFound();

            return Ok(stockItem);
        }

        [HttpPost]
        [Authorize("SystemMessagingPolicy")]
        public void Post([FromBody]StockItemDto item)
        {
            _commandMediator.Send(CreateCommandForState(item));
        }

        [HttpPut("{id:Guid}")]
        public IActionResult Put(Guid id, [FromBody]StockItemDto newState)
        {
            // Set the Id from the URI to the DTO to send on
            newState.Id = id;

            // Ensure the current user is the stock owner
            newState.OwnerId = GetUserId();

            try
            {
                _commandMediator.Send(CreateCommandForState(newState));
                return Created($"/api/stockitem/{id}", _queryMediator.Get(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            // May need another resource to post to instead of overloading this delete verb
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

        private ICommand<StockItem> CreateCommandForState(StockItemDto newState)
        {
            if (_queryMediator.Get(newState.Id) == null)
                return new CreateStockItemCommand(newState, GetUserId());

            return new UpdateStockItemCommand(newState, GetUserId());
        }
    }
}