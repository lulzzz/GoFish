using GoFish.Shared.Dto;

namespace GoFish.Advert
{
    public abstract class AdvertBuilder
    {
        protected readonly AdvertDto Data;

        public AdvertBuilder (AdvertDto data)
        {
            Data = data;
        }
        public abstract Advert Build();
    }
}