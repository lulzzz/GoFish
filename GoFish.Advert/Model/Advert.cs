using System;
using System.Collections.Generic;

namespace GoFish.Advert
{
    public class Advert
    {
        private IList<AdvertEvent> _changes = new List<AdvertEvent>();

        private Advert() { }

        internal Advert(Guid id, CatchType catchType, int quantity, double price, Advertiser advertiser)
        {
            Id = id;
            CatchType = catchType;
            Quantity = quantity;
            Price = price;
            Advertiser = advertiser;
            Status = AdvertStatus.Creating;
        }

        internal Advert(IEnumerable<AdvertEvent> evenory)
        {
            foreach (var item in evenory)
            {
                Apply(item, false);
            }
        }

        public Guid Id { get; private set; }
        public CatchType CatchType { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public Advertiser Advertiser { get; private set; }
        public IList<AdvertEvent> History { get; } = new List<AdvertEvent>();
        public string Pitch { get; internal set; }
        public FishingMethod FishingMethod { get; internal set; }
        public AdvertStatus Status { get; internal set; }

        public void Create()
        {
            if (Status != AdvertStatus.Creating)
            {
                throw new InvalidOperationException($"Cannot set status to Created from {Status.ToString()}");
            }

            Apply(new AdvertCreatedEvent(Id, Advertiser, CatchType, Pitch), true);
        }

        public void Post()
        {
            if (Status != AdvertStatus.Created)
            {
                throw new InvalidOperationException("Can only post non-posted & non-published adverts.");
            }

            Apply(new AdvertPostedEvent(Id), true);
        }

        public void Publish()
        {
            Apply(new AdvertPublishedEvent(Id), true);
        }

        private void Apply(AdvertEvent @event, bool isNew)
        {
            ((dynamic)this).When((dynamic)@event);
            if (isNew) _changes.Add(@event);
            History.Add(@event);
        }

        public IList<AdvertEvent> GetChanges() { return _changes;  }

        private void When(AdvertCreatedEvent e)
        {
            Advertiser = e.Advertiser;
            CatchType = e.CatchType;
            Pitch = e.Pitch;
            Status = AdvertStatus.Created;
        }

        private void When(AdvertPostedEvent e)
        {
            Status = AdvertStatus.Posted;
        }

        private void When(AdvertPublishedEvent e)
        {
            Status = AdvertStatus.Published;
        }
    }
}