using System;

namespace GoFish.Advert
{
    public class AdvertPublishedEvent : AdvertEvent
    {
        public readonly int StockQuantity;
        public AdvertPublishedEvent(Guid id, int stockQuantity) : base(id)
        {
            StockQuantity = stockQuantity;
        }
    }
}
