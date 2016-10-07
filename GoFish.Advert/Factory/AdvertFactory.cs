using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public class AdvertFactory : IAdvertFactory
    {
        public Advert BuildNew(AdvertDto fromDto)
        {
            var resultingAdvert = new Advert(
                fromDto.Id,
                CatchType.FromId((int)fromDto.CatchTypeId),
                (int)fromDto.Quantity,
                (double)fromDto.Price,
                Advertiser.FromId((int)fromDto.AdvertiserId));

            resultingAdvert.Pitch = fromDto.Pitch;
            resultingAdvert.FishingMethod = (FishingMethod)fromDto.FishingMethod;

            return resultingAdvert;
        }

        public Advert Update(Advert advert, AdvertDto fromDto)
        {
            var resultingAdvert = BuildNew(fromDto);
            resultingAdvert.Status = advert.Status;
            resultingAdvert.Update();
            return resultingAdvert;
        }
    }
}