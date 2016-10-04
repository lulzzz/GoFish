using System;
using System.Net;
using System.Net.Http;
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
            return Ok(_query.GetPosted());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var advert = _query.Get(id);

            if (advert == null || advert.Status != AdvertStatus.Posted)
                return NotFound();

            return Ok(advert);
        }

        [HttpPut("{id}")]
        public HttpResponseMessage PostAdvert(Guid id)
        {
            try
            {
                _command.Send(new PostAdvertCommand(id));
                return new HttpResponseMessage(HttpStatusCode.Accepted)
                {
                    ReasonPhrase = "Advert Posted for Publishing",

                };
            }
            catch (AdvertNotFoundException)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (InvalidOperationException ex)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = ex.Message
                };
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound); // Better a 404 than a potential hack vector.
            }
        }
    }
}