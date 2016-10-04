using System;
using System.Collections.Generic;

namespace GoFish.Advert
{
    public class Advert
    {
        private Advert() { }

        public Advert(Guid id, CatchType catchType, int quantity, double price, Advertiser advertiser)
        {
            Id = id;
            CatchType = catchType;
            Quantity = quantity;
            Price = price;
            Advertiser = advertiser;
            Status = AdvertStatus.Creating;
        }

        public Guid Id { get; private set; }
        public CatchType CatchType { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public Advertiser Advertiser { get; private set; }
        public string Pitch { get; internal set; }
        public FishingMethod FishingMethod { get; internal set; }
        public AdvertStatus Status { get; internal set; }
        public IList<AdvertEvent> NewEvents { get; private set; }

        public void Create()
        {
            if (Status != AdvertStatus.Creating)
            {
                throw new InvalidOperationException($"Cannot set status to Created from {Status.ToString()}");
            }
            Status = AdvertStatus.Created;
            NewEvents.Add(new AdvertEvent(AdvertEventType.AdvertCreated ,Id, SerializeThis()));
        }

        public void Post()
        {
            Status = AdvertStatus.Posted;
            NewEvents.Add(new AdvertEvent(AdvertEventType.AdvertPosted ,Id, SerializeThis()));
        }

        public void Publish()
        {
            Status = AdvertStatus.Published;
            NewEvents.Add(new AdvertEvent(AdvertEventType.AdvertPublished ,Id, SerializeThis()));
        }

        private string SerializeThis()
        {
            // used to store the object as a json blob in a key-value style event store
            throw new NotImplementedException();
        }
    }

    public class AdvertEvent
    {
        public readonly Guid Id;
        public readonly string EventName;
        public readonly string Payload;

        public AdvertEvent(AdvertEventType eventName, Guid id, string payload)
        {
            Id = id;
            EventName = eventName.ToString();
            Payload = payload;
        }
    }

    public enum AdvertEventType
    {
        AdvertCreated,
        AdvertPosted,
        AdvertPublished
    }
}