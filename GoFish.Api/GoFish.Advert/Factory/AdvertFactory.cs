using System;
using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class AdvertFactory : IAdvertFactory
    {
        private readonly ILookupCacheProvider _cache;

        public AdvertFactory (ILookupCacheProvider cache)
        {
            _cache = cache;
        }

        public Advert BuildNew(AdvertDto buildData)
        {
            ValidateInput(buildData);

            var resultingAdvert = new Advert(
                buildData.Id,
                LookupItem.GetFromCache<CatchType>(_cache, (int)buildData.CatchTypeId),
                (int)buildData.Quantity,
                (double)buildData.Price,
                LookupItem.GetFromCache<Advertiser>(_cache, (int)buildData.AdvertiserId));

            resultingAdvert.Pitch = buildData.Pitch;
            resultingAdvert.FishingMethod = (FishingMethod)(buildData.FishingMethod ?? (int)FishingMethod.Unknown);

            return resultingAdvert;
        }

        public Advert Update(Advert advert, AdvertDto fromDto)
        {
            var resultingAdvert = BuildNew(fromDto);
            resultingAdvert.Status = advert.Status;
            resultingAdvert.Update();
            return resultingAdvert;
        }

        private static void ValidateInput(AdvertDto fromDto)
        {
            if (fromDto.CatchTypeId == null)
                throw new ArgumentNullException("Catch Type");

            if (fromDto.CatchTypeId == 0)
                throw new ArgumentOutOfRangeException("Catch Type");
        }
    }
}