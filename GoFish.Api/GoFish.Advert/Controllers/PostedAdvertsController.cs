using System;
using System.Net;
using GoFish.Shared.Command;
using GoFish.Shared.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class PostedAdvertsController : ApiBaseController
    {
        private readonly ICommandMediator _command;
        private readonly AdvertRepository _query;

        public PostedAdvertsController(ICommandMediator commandMediator, AdvertRepository repository)
        {
            _command = commandMediator;
            _query = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_query.GetPosted(GetUserId()));
        }

        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            var advert = _query.Get(id);

            if (advert == null || advert.Status != AdvertStatus.Posted)
                return NotFound();

            return Ok(advert);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult PostAdvert(Guid id, [FromBody]PostAdvertPayload data)
        {
            try
            {
                _command.Send(new PostAdvertCommand(id, GetUserId(), data.PublishType=="PublishAndAdd", data.StockQuantity));
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

    public class PostAdvertPayload
    {
        public string PublishType { get; set; }
        public int StockQuantity { get; set; }
    }
}