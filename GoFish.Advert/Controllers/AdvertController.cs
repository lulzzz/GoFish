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
            var theCatch = _dbContext.Adverts
                .Where(c => c.Id == id)
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .SingleOrDefault();

            return new ObjectResult(theCatch);
        }

        [HttpPost]
        public void Post([FromBody]Advert item)
        {
            using (var db = _dbContext)
            {
                db.Adverts.Add(item);
                db.CatchTypes.Attach(item.CatchType);
                db.Advertisers.Attach(item.Advertiser);
                db.SaveChanges();
            }
        }

        [HttpPut("{id}/post")]
        public void AdvertPosted(int id)
        {
            Advert item;
            using (var db = _dbContext)
            {
                item = db.Adverts
                    .Include(a => a.Advertiser)
                    .Include(ct => ct.CatchType)
                    .Single(a => a.Id == id);

                item.Post();
                db.SaveChanges();
            }

            _messageBroker.Send(item);
        }


        [HttpPut("{id}/publish")]
        public void AdvertPublished(int id)
        {
            using (var db = _dbContext)
            {
                db.Adverts.Single(a => a.Id == id).Publish();
                db.SaveChanges();
            }
        }
    }
}