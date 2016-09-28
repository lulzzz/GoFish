using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public abstract class AdvertFactory
    {
        protected readonly AdvertDto Data;
        protected Advert CreatedAdvert;

        public AdvertFactory (AdvertDto data)
        {
            Data = data;
        }

        public abstract Advert Build();

        public void TransferCommonProperties()
        {
            CreatedAdvert.Pitch = Data.Pitch;
            CreatedAdvert.FishingMethod = (FishingMethod)Data.FishingMethodId;
        }
    }
}