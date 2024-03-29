using System;
using System.Collections.Generic;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class Advert: ICommandable
    {
        private IList<AdvertEvent> _changes = new List<AdvertEvent>();

        private Advert() { }

        internal Advert(Guid id, CatchType catchType, double price, Advertiser advertiser)
        {
            Id = id;
            CatchType = catchType;
            Price = price;
            Advertiser = advertiser;
            Status = AdvertStatus.Unknown;
        }

        internal Advert(Guid id, IEnumerable<AdvertEvent> history)
        {
            Id = id;
            foreach (var item in history)
            {
                Apply(item, false);
            }
        }

        public Guid Id { get; private set; }
        public CatchType CatchType { get; private set; }
        public double Price { get; private set; }
        public Advertiser Advertiser { get; private set; }
        public IList<AdvertEvent> History { get; } = new List<AdvertEvent>();
        public string Pitch { get; internal set; }
        public FishingMethod FishingMethod { get; internal set; }
        public AdvertStatus Status { get; internal set; }
        public int StockQuantity { get; set; }

        public void Create()
        {
            if (Status != AdvertStatus.Unknown)
            {
                throw new InvalidOperationException($"Cannot set advert status to Created from {Status.ToString()}");
            }

            Apply(new AdvertCreatedEvent(
                Id,
                CatchType,
                Price,
                Advertiser,
                Pitch,
                FishingMethod), isNewEvent: true);
        }

        public void Update() // All in one for now (or there's gonna be a whole load of set() methods to create!)
        {
            if (Status != AdvertStatus.Created)
            {
                throw new InvalidOperationException($"Cannot update the details of an advert in {Status.ToString()} status");
            }

            Apply(new AdvertUpdatedEvent(
                Id,
                CatchType,
                Price,
                Advertiser,
                Pitch,
                FishingMethod,
                Status), isNewEvent: true);
        }

        public void Post()
        {
            if (Status != AdvertStatus.Created)
            {
                throw new InvalidOperationException("Can only post non-posted & non-published adverts.");
            }

            Apply(new AdvertPostedEvent(Id), isNewEvent: true);
        }

        public void StockLevelChanged(int stockLevel)
        {
            Apply(new StockLevelChangedEvent(Id, stockLevel), isNewEvent: true);
        }

        public void PostToStock(int stockQuantity)
        {
            if (Status != AdvertStatus.Posted)
            {
                throw new InvalidOperationException("Can only add a posted advert to stock.");
            }

            Apply(new AdvertPostedToStockEvent(Id, stockQuantity), isNewEvent: true);
        }

        public void Publish(int stockQuantity)
        {
            Apply(new AdvertPublishedEvent(Id, stockQuantity), true);
        }

        public void Withdraw()
        {
            Apply(new AdvertWithdrawnEvent(Id), true);
        }

        private void Apply(AdvertEvent @event, bool isNewEvent)
        {
            ((dynamic)this).When((dynamic)@event);
            if (isNewEvent) _changes.Add(@event);
            History.Add(@event);
        }

        public IList<AdvertEvent> GetChanges()
        {
            return _changes;
        }

        private void When(AdvertCreatedEvent e)
        {
            Id = e.Id;
            CatchType = e.CatchType;
            Price = e.Price;
            Advertiser = e.Advertiser;
            Pitch = e.Pitch;
            FishingMethod = e.FishingMethod;
            Status = AdvertStatus.Created;
        }

        private void When(AdvertUpdatedEvent e)
        {
            Id = e.Id;
            CatchType = e.CatchType;
            Price = e.Price;
            Advertiser = e.Advertiser;
            Pitch = e.Pitch;
            FishingMethod = e.FishingMethod;
            Status = e.Status;
        }

        private void When(AdvertPostedEvent e)
        {
            Status = AdvertStatus.Posted;
        }

        private void When(AdvertPostedToStockEvent e)
        {
            Status = AdvertStatus.Posted;
            StockQuantity = e.StockQuantity;
        }

        private void When(AdvertPublishedEvent e)
        {
            Status = AdvertStatus.Published;
            StockQuantity = e.StockQuantity;
        }

        private void When(AdvertWithdrawnEvent e)
        {
            Status = AdvertStatus.Withdrawn;
        }

        private void When(StockLevelChangedEvent e)
        {
            StockQuantity = e.StockLevel;
        }
    }
}