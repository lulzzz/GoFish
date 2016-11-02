using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFish.Advert
{
    public abstract class LookupItem
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }

        public static T GetFromCache<T>(ILookupCacheProvider cache, int lookupId) where T : LookupItem
        {
            LookupItem item;
            Type lookupType = typeof(T);

            if (lookupType == typeof(CatchType))
            {
                item = cache.CatchTypes.Where(i => i.Id == lookupId).SingleOrDefault();
            }
            else
            {
                item = cache.Advertisers.Where(i => i.Id == lookupId).SingleOrDefault();
            }

            return (T)Activator.CreateInstance(typeof(T), new object[] { item.Id, item.Name });
        }
    }

    public interface ILookupCacheProvider
    {
        IEnumerable<CatchType> CatchTypes { get; }
        IEnumerable<Advertiser> Advertisers { get; }
    }

    public class LookupCacheProvider : ILookupCacheProvider
    {
        private readonly AdvertisingDbContext _dataSource; // Change to REDIS

        public LookupCacheProvider (AdvertisingDbContext dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Advertiser> Advertisers
        {
            get
            {
                return _dataSource.Advertisers;
            }
        }

        public IEnumerable<CatchType> CatchTypes
        {
            get
            {
                return _dataSource.CatchTypes;
            }
        }
    }
}