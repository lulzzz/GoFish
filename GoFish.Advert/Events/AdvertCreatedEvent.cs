using System;

namespace GoFish.Advert
{
    public class AdvertCreatedEvent : AdvertEvent
    {
        public readonly CatchType CatchType;
        public readonly int Quantity;
        public readonly double Price;
        public readonly Advertiser Advertiser;
        public readonly string Pitch;
        public readonly FishingMethod FishingMethod;

        public AdvertCreatedEvent(
            Guid id,
            CatchType catchType,
            int quantity,
            double price,
            Advertiser advertiser,
            string pitch,
            FishingMethod fishingMethod
            ) : base(id)
        {
            CatchType = catchType;
            Quantity = quantity;
            Price = price;
            Advertiser = advertiser;
            Pitch = pitch;
            FishingMethod = fishingMethod;
        }
    }
}