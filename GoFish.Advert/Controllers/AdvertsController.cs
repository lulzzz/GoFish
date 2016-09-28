using GoFish.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class AdvertsController : Controller
    {
        private readonly AdvertRepository _repository;

        public AdvertsController(AdvertRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetDraftAdverts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var advert = _repository.Get(id);

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

            Advert advert;
            try
            {
                advert = SaveAdvert(new CreateAdvertFactory(item));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedResult(advert);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AdvertDto newState)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id == 0)
                return BadRequest("Incorrect use of PUT to add a new Item.  POST to the collection instead.");

            var advert = _repository.Get(id);

            if (advert == null)
                return NotFound();

            try
            {
                advert = SaveAdvert(new UpdateAdvertFactory(advert, newState));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedResult(advert);
        }

        private IActionResult CreatedResult(Advert advert)
        {
            return Created($"/api/{GetControllerName()}/{advert.Id}", advert);
        }

        private Advert SaveAdvert(AdvertFactory command)
        {
            return _repository.Save(command.Build());

        }
        private string GetControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
        }
    }
}