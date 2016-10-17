using System;

namespace GoFish.Advert
{
    public class AdvertWithdrawnEvent : AdvertEvent
    {
        public AdvertWithdrawnEvent(Guid id) : base(id) { }
    }
}
