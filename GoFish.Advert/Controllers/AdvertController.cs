using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    [Route("api/[controller]")]
    public class AdvertController : Controller
    {
        private readonly AdvertisingDbContext _dbContext;
        private readonly IMessageBroker<Advert> _messageBroker;

        public AdvertController(AdvertisingDbContext dbContext, IMessageBroker<Advert> messageBroker)
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
        public void Post([FromBody]Advert item)
        {
            _dbContext.Adverts.Add(item);
            _dbContext.CatchTypes.Attach(item.CatchType);
            _dbContext.Advertisers.Attach(item.Advertiser);
            _dbContext.SaveChanges();

            _messageBroker.Send(item);
        }

        [HttpPut("{id}/publish")]
        public void StockAdded(int id)
        {
            _dbContext.Adverts.Single(a => a.Id == id).PublishAdvert();
            _dbContext.SaveChanges();
        }
    }
}