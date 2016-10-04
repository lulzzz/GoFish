using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class PublishedAdvertsController : ApiBaseController
    {
        private readonly ICommandMediator _command;
        private readonly AdvertRepository _query;

        public PublishedAdvertsController(ICommandMediator commandMediator, AdvertRepository repository)
        {
            _command = commandMediator;
            _query = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_query.GetPublished());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var advert = _query.Get(id);

            if (advert == null || advert.Status != AdvertStatus.Published)
                return NotFound();

            return Ok(advert);
        }

        [HttpPut("{id}")]
        public HttpResponseMessage PublishAdvert(Guid id)
        {
            try
            {
                _command.Send(new PublishAdvertCommand(id));
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