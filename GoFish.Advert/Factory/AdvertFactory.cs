using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public abstract class AdvertFactory
    {
        protected readonly AdvertDto Data;
        protected Advert ResultingAdvert;

        public AdvertFactory (AdvertDto data)
        {
            Data = data;
        }

        public abstract Advert Build();

        public void TransferCommonProperties()
        {
            ResultingAdvert.Pitch = Data.Pitch;
            ResultingAdvert.FishingMethod = (FishingMethod)Data.FishingMethodId;
        }
    }
}