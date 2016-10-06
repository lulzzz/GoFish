using System;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class WithdrawnAdvertsController : ApiBaseController
    {
        private readonly AdvertRepository _query;

        public WithdrawnAdvertsController(AdvertRepository repository)
        {
            _query = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_query.GetWithdrawn());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var advert = _query.Get(id);

            if (advert == null || advert.Status != AdvertStatus.Withdrawn)
                return NotFound();

            return Ok(advert);
        }
    }
}