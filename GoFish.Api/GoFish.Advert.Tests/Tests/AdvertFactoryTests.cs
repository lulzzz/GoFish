using System;
using GoFish.Advert;
using GoFish.Shared.Dto;
using Xunit;

namespace GoFish.Api.Tests
{
    public class AdvertFactoryTests
    {
        [Fact]
        public void WhenCreatingAdvert_CatchTypeIdMustBeProvided()
        {
            var factory = new AdvertFactory();
            var data = CreateBasicDto();
            data.CatchTypeId = null;

            var ex = Assert.Throws<ArgumentNullException>(() => factory.BuildNew(data));
            Assert.Equal("Catch Type", ex.ParamName);
        }

        [Fact]
        public void WhenCreatingAdvert_CatchTypeIdMustBeGreaterThanZero()
        {
            var factory = new AdvertFactory();
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
                Quantity = 1,
                Price = 1,
                AdvertiserId = 1,
                FishingMethod = (int)FishingMethod.Unknown
            };
        }
    }
}
