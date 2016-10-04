using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public interface IAdvertFactory
    {
        Advert Build();
    }

    public abstract class AdvertFactory : IAdvertFactory
    {
        protected readonly AdvertDto Data;
        protected Advert ResultingAdvert;

        public AdvertFactory (AdvertDto data)
        {
            Data = data;
        }

        public abstract Advert Build();

        protected void TransferCommonProperties()
        {
            ResultingAdvert.Pitch = Data.Pitch;
            ResultingAdvert.FishingMethod = (FishingMethod)Data.FishingMethodId;
            ResultingAdvert.Status = (AdvertStatus)Data.Status;
        }
    }
}