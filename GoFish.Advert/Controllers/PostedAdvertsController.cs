using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class PostedAdvertsController : Controller
    {
        private readonly ICommandMediator _commandMediator;
        private readonly AdvertRepository _repository; // TODO: Can remove this with a QueryMediator

        public PostedAdvertsController(ICommandMediator commandMediator, AdvertRepository repository)
        {
            _commandMediator = commandMediator;
            _repository = repository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetPosted());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var advert = _repository.Get(id);

            if (advert == null || advert.Status != AdvertStatus.Posted)
                return new NotFoundResult();

            return Ok(advert);
        }

        [HttpPut("{id}")]
        public IActionResult PostAdvert(int id)
        {
            try
            {
                var advert = _commandMediator.Send(new PostAdvertCommand(id));
                return Created($"/api/postedadverts/{advert.Id}", advert);
            }
            catch(AdvertNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return NotFound(); // Is this right?  Better a 404 than a potential hack target?
            }
        }
    }
}