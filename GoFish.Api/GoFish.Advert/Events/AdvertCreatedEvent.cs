using System;

namespace GoFish.Advert
{
    public class AdvertCreatedEvent : AdvertEvent
    {
        public readonly CatchType CatchType;
        public readonly double Price;
        public readonly Advertiser Advertiser;
        public readonly string Pitch;
        public readonly FishingMethod FishingMethod;

        public AdvertCreatedEvent(
            Guid id,
            CatchType catchType,
            double price,
            Advertiser advertiser,
            string pitch,
            FishingMethod fishingMethod
            ) : base(id)
        {
            CatchType = catchType;
            Price = price;
            Advertiser = advertiser;
            Pitch = pitch;
            FishingMethod = fishingMethod;
        }
    }
}