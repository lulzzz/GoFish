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
            return _dbContext.Adverts.AsNoTracking()
                .Include(ct => ct.CatchType)
                .Include(a => a.Advertiser)
                .SingleOrDefault(a => a.Id == id);
        }

        internal object GetDraftAdverts()
        {
            return _dbContext.Adverts
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Created);
        }

        internal IEnumerable<Advert> GetPublished()
        {
            return _dbContext.Adverts
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Published);
        }

        internal object GetPosted()
        {
            return _dbContext.Adverts
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Posted);
        }

        internal Advert Save(Advert item)
        {
            // Check data - validation needs to go somewhere.
            // could DDD the Advert so it's always valid, or
            // put a load of checks here or a validator component
            if (item.Id == 0)
            {
                _dbContext.Adverts.Add(item);
                _dbContext.CatchTypes.Attach(item.CatchType);
                _dbContext.Advertisers.Attach(item.Advertiser);
            }
            else
            {
                _dbContext.Adverts.Attach(item);
                _dbContext.Entry(item).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();

            return Get(item.Id);
        }
    }
}