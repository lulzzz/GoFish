using System;

namespace GoFish.Advert
{
    public abstract class AdvertEvent
    {
        public readonly Guid _id;

        public AdvertEvent(Guid id)
        {
            _id = id;
        }
    }

    public class AdvertCreatedEvent : AdvertEvent
    {
        public readonly Advertiser Advertiser;
        public readonly CatchType CatchType;
        public readonly string Pitch;

        public AdvertCreatedEvent(Guid id, Advertiser advertiser, CatchType catchType, string pitch) : base(id)
        {
            Advertiser = advertiser;
            CatchType = catchType;
            Pitch = pitch;
        }
    }

    public class AdvertPostedEvent : AdvertEvent
    {
        public AdvertPostedEvent(Guid id) : base(id) { }
    }

    public class AdvertPublishedEvent : AdvertEvent
    {
        public AdvertPublishedEvent(Guid id) : base(id) { }
    }
}