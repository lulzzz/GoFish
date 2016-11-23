using GoFish.Shared.Dto;
using Xunit;

namespace GoFish.Advert.Tests
{
    public class AdvertTests
    {
        [Fact]
        public void NewAdvertHasStatusUnknown()
        {
            var cache = new CacheProviderStub();
            var factory = new AdvertFactory(cache);
            var data = CreateBasicDto();

            var sut = factory.BuildNew(data);
            Assert.Equal(sut.Status, GoFish.Advert.AdvertStatus.Unknown);
        }

        private AdvertDto CreateBasicDto()
        {
            return new AdvertDto()
            {
                CatchTypeId = 1,
                Price = 1,
                AdvertiserId = 1,
                FishingMethod = (int)FishingMethod.Unknown
            };
        }
    }
}