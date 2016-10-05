using System;

namespace GoFish.Advert
{
    public abstract class AdvertEvent
    {
        public readonly Guid Id;

        public AdvertEvent(Guid id)
        {
            Id = id;
        }
    }
}