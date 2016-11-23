using System;
using GoFish.Shared.Dto;
using Xunit;

namespace GoFish.Advert.Tests
{
    public class AdvertFactoryTests
    {
        [Fact]
        public void WhenCreatingAdvert_CatchTypeIdMustBeProvided()
        {
            var cache = new CacheProviderStub();
            var factory = new AdvertFactory(cache);
            var data = CreateBasicDto();
            data.CatchTypeId = null;

            var ex = Assert.Throws<ArgumentNullException>(() => factory.BuildNew(data));
            Assert.Equal("Catch Type", ex.ParamName);
        }

        [Fact]
        public void WhenCreatingAdvert_CatchTypeIdMustBeGreaterThanZero()
        {
            var cache = new CacheProviderStub();
            var factory = new AdvertFactory(cache);
            var data = CreateBasicDto();
            data.CatchTypeId = 0;

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => factory.BuildNew(data));
            Assert.Equal("Catch Type", ex.ParamName);
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