using System;
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
            var adverts = _query.GetPosted();

            if (adverts == null)
                return NotFound();

            return Ok(adverts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var advert = _query.Get(id);

            if (advert == null || advert.Status != AdvertStatus.Posted)
                return NotFound();

            return Ok(advert);
        }

        [HttpPut("{id}")]
        public IActionResult PostAdvert(int id)
        {
            try
            {
                var advert = _command.Send(new PostAdvertCommand(id));
                return Created($"/api/{GetControllerName()}/{id}", advert);
            }
            catch (AdvertNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return NotFound(); // Better a 404 than a potential hack target.
            }
        }
    }
}