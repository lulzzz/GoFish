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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_repository.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]AdvertDto item)
        {
            if (item.Id != 0)
                return BadRequest("Incorrect use of POST to update an Item.  PUT to the resource instead.");

            Advert advert;
            try
            {
                advert = SaveAdvert(new CreateAdvertBuilder(item));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedResult(advert);
        }

        [HttpPut]
        public IActionResult Put([FromBody]AdvertDto item)
        {
            if (item.Id == 0)
                return BadRequest("Incorrect use of PUT to add a new Item.  POST to the collection instead.");

            Advert advert;
            try
            {
                advert = SaveAdvert(new UpdateAdvertBuilder(item));
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

        private Advert SaveAdvert(AdvertBuilder command)
        {
            return _repository.Save(command.Build());

        }
        private string GetControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
        }
    }
}