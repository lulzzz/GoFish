using System;
using System.Net;
using GoFish.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class AdvertsController : ApiBaseController
    {
        private readonly ICommandMediator _commandMediator;
        private readonly AdvertRepository _queryMediator;

        public AdvertsController(ICommandMediator commandMediator, AdvertRepository repository)
        {
            _commandMediator = commandMediator;
            _queryMediator = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]AdvertSearchOptions options)
        {
            if (!Request.QueryString.HasValue)
                return Ok(_queryMediator.GetDraftAdverts());

            try
            {
                return Ok(_queryMediator.Search(options));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            var advert = _queryMediator.Get(id);

            if (advert == null)
                return NotFound();

            return Ok(advert);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]AdvertDto newState)
        {
            if (!ModelState.IsValid) return BadRequest();

            // Set the Id from the URI to the DTO to send on
            if (newState.Id == Guid.Empty) newState.Id = id;

            try
            {
                _commandMediator.Send(CreateCommandForState(newState));
                return Created($"/api/{GetControllerName()}/{id}", _queryMediator.Get(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Withdraw(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _commandMediator.Send(new WithdrawAdvertCommand(id));
                return new StatusCodeResult((int)HttpStatusCode.Accepted);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private ICommand<Advert> CreateCommandForState(AdvertDto newState)
        {
            if (_queryMediator.Get(newState.Id) == null)
                return new CreateAdvertCommand(newState);

            return new UpdateAdvertCommand(newState);
        }
    }
}