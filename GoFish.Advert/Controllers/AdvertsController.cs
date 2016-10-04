using System;
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
        public IActionResult Get()
        {
            return Ok(_queryMediator.GetDraftAdverts());
        }

        [HttpGet("{id}")]
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

            if (newState.Id == Guid.Empty) newState.Id = id;

            try
            {
                _commandMediator.Send(CreateCommandForState(newState));
                return Created($"/api/{GetControllerName()}/{id}", _queryMediator.Get(id));
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        private ICommand<Advert> CreateCommandForState(AdvertDto newState)
        {
            var existing = _queryMediator.Get(newState.Id);
            var factory = CreateFactory(existing, newState);

            if (existing == null)
                return new CreateAdvertCommand(factory.Build());

            return new UpdateAdvertCommand(factory.Build());
        }

        private IAdvertFactory CreateFactory(Advert existing, AdvertDto newState)
        {
            if (existing == null)
                return new CreateAdvertFactory(newState);

            return new UpdateAdvertFactory(existing, newState);
        }
    }
}