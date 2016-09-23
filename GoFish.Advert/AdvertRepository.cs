using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GoFish.Advert
{
    public class AdvertRepository
    {
        private readonly AdvertisingDbContext _dbContext;

        public AdvertRepository(AdvertisingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal Advert Get(int id)
        {
            return _dbContext.Adverts
                .Include(ct => ct.CatchType)
                .Include(a => a.Advertiser)
                .SingleOrDefault(a => a.Id == id);
        }

        internal IEnumerable<Advert> GetPublished()
        {
            return _dbContext.Adverts.Where(s => s.Status == AdvertStatus.Published);
        }

        internal object GetPosted()
        {
            return _dbContext.Adverts.Where(s => s.Status == AdvertStatus.Posted);
        }

        internal Advert Save(Advert item)
        {
            if (item.Id == 0)
            {
                _dbContext.Adverts.Add(item);
                _dbContext.CatchTypes.Attach(item.CatchType);
                _dbContext.Advertisers.Attach(item.Advertiser);
            }

            _dbContext.SaveChanges();

            return item;
        }
    }
}