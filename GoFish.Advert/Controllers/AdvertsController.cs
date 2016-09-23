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
        public IActionResult Post([FromBody]Advert item)
        {
            item = _repository.Save(item);
            return Created($"/api/adverts/{item.Id}", item);
        }
    }
}