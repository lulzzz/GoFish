using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class PublishedAdvertsController : Controller
    {
        private readonly AdvertRepository _repository;

        public PublishedAdvertsController(AdvertRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetPublished());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _repository.Get(id);

            if (item == null || item.Status != AdvertStatus.Published)
                return new NotFoundResult();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult PublishAdvert(int id)
        {
            var advert = _repository.Get(id);

            if (advert == null || advert.Status != AdvertStatus.Posted)
                return new NotFoundResult();

            advert.Publish();

            _repository.Save(advert);

            return Created($"/api/publishedadverts/{id}", advert);
        }
    }
}