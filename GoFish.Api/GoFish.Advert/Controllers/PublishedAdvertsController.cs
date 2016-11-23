using System;
using System.Net;
using GoFish.Shared.Command;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class PublishedAdvertsController : ApiBaseController
    {
        private readonly ICommandMediator _command;
        private readonly AdvertRepository _query;

        public PublishedAdvertsController(ICommandMediator commandMediator, AdvertRepository repository)
        {
            _command = commandMediator;
            _query = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_query.GetPublished());
        }

        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            var advert = _query.Get(id);

            if (advert == null || advert.Status != AdvertStatus.Published)
                return NotFound();

            return Ok(advert);
        }

        [HttpPut("{id:Guid}")]
        [Authorize("SystemMessagingPolicy")]
        public IActionResult PublishAdvert(Guid id, [FromBody]StockItemDto data)
        {
            try
            {
                _command.Send(new PublishAdvertCommand(id, (int)data.Quantity));
                return new StatusCodeResult((int)HttpStatusCode.Accepted);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return NotFound(); // Better a 404 than a potential hack vector.
            }
        }

        [HttpPut("[action]/{id:Guid}")]
        [Authorize("SystemMessagingPolicy")]
        public IActionResult StockChange(Guid id, [FromBody]StockChangeDto data)
        {
            try
            {
                _command.Send(new StockUpdatedCommand(data.AdvertId, data.Quantity));
                return new StatusCodeResult((int)HttpStatusCode.Accepted);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return NotFound(); // Better a 404 than a potential hack vector.
            }
        }

    }
}