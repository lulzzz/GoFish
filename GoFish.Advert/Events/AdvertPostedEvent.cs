using System;

namespace GoFish.Advert
{
    public class AdvertPostedEvent : AdvertEvent
    {
        public AdvertPostedEvent(Guid id) : base(id) { }
    }
}