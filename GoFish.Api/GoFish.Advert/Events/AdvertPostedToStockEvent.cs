using System;

namespace GoFish.Advert
{
    public class AdvertPostedToStockEvent : AdvertEvent
    {
        public AdvertPostedToStockEvent(Guid id) : base(id) { }
    }
}