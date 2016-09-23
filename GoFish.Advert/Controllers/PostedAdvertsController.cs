using Microsoft.AspNetCore.Mvc;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class PostedAdvertsController : Controller
    {
        private readonly AdvertRepository _repository;
        private readonly IMessageBroker<Advert> _messageBroker;

        public PostedAdvertsController(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetPosted());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var advert = _repository.Get(id);

            if (advert.Status != AdvertStatus.Posted)
                return new NotFoundResult();

            return Ok(advert);
        }

        [HttpPut("{id}")]
        public IActionResult PostAdvert(int id)
        {
            var advert = _repository.Get(id);

            if (advert == null)
                return new NotFoundResult();

            if (advert.Status != AdvertStatus.Created)
                return new NotFoundResult();

            advert.Post();

            _repository.Save(advert);

            _messageBroker.Send(advert);

            return Created($"/api/postedadverts/{advert.Id}", advert);
        }
    }
}