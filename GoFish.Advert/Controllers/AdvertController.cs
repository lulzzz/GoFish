using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class AdvertController : Controller
    {
        private readonly AdvertisingContext _dbContext;
        private readonly IMessageBroker<Advert> _messageBroker;

        public AdvertController(AdvertisingContext dbContext, IMessageBroker<Advert> messageBroker)
        {
            _dbContext = dbContext;
            _messageBroker = messageBroker;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var theCatch = _dbContext.Adverts.Where(c => c.Id == id)
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .SingleOrDefault();

            return new ObjectResult(theCatch);
        }

        [HttpPost]
        public void Post([FromBody]Advert advert)
        {
            _dbContext.Adverts.Add(advert);
            _dbContext.CatchTypes.Attach(advert.CatchType);
            _dbContext.Advertisers.Attach(advert.Advertiser);
            _dbContext.SaveChanges();

            _messageBroker.Send(advert);
        }
    }
}