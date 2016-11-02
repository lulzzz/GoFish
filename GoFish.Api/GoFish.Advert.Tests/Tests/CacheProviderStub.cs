using System.Collections.Generic;

namespace GoFish.Advert.Tests
{
    public class CacheProviderStub : ILookupCacheProvider
    {
        public IEnumerable<Advertiser> Advertisers
        {
            get
            {
                return new List<Advertiser>() {
                    new Advertiser(1, "Netter Nina")
                };
            }
        }

        public IEnumerable<CatchType> CatchTypes
        {
            get
            {
                return new List<CatchType>() {
                    new CatchType(1, "Lobster")
                };
            }
        }
    }
}