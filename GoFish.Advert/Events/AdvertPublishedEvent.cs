using System;

namespace GoFish.Advert
{
    public class AdvertPublishedEvent : AdvertEvent
    {
        public AdvertPublishedEvent(Guid id) : base(id) { }
    }
}
