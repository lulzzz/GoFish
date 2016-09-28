using GoFish.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class AdvertsController : ApiBaseController
    {
        private readonly ICommandMediator _command;
        private readonly AdvertRepository _query;

        public AdvertsController(ICommandMediator commandMediator, AdvertRepository repository)
        {
            _command = commandMediator;
            _query = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var adverts = _query.GetDraftAdverts();

            if (adverts == null)
                return NotFound();

            return Ok(adverts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var advert = _query.Get(id);

            if (advert == null)
                return NotFound();

            return Ok(advert);
        }

        [HttpPost]
        public IActionResult Post([FromBody]AdvertDto item)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (item.Id != 0)
                return BadRequest("Incorrect use of POST to update an Item.  PUT to the resource instead.");

            return SaveAdvert(new CreateAdvertFactory(item));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AdvertDto newState)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id == 0)
                return BadRequest("Incorrect use of PUT to add a new Item.  POST to the collection instead.");

            var advert = _query.Get(id);

            if (advert == null)
                return NotFound();

            return SaveAdvert(new UpdateAdvertFactory(advert, newState));
        }

        private IActionResult SaveAdvert(AdvertFactory advertFactory)
        {
            Advert advert;
            try
            {
                advert = _command.Send(new SaveAdvertCommand(advertFactory.Build()));
                return Created($"/api/{GetControllerName()}/{advert.Id}", advert);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}