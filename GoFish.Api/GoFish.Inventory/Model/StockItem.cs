using System;
using System.Collections.Generic;
using GoFish.Shared.Interface;

namespace GoFish.Inventory
{
    public class StockItem : ICommandable
    {
        private IList<StockItemEvent> _changes = new List<StockItemEvent>();

        private StockItem() { }

        public StockItem(Guid id, ProductType productType, int quantity, double price, StockOwner owner, Guid advertId)
        {
            Id = id;
            ProductType = productType;
            Quantity = quantity;
            Price = price;
            Owner = owner;
            AdvertId = advertId;
        }

        public StockItem(Guid id, IEnumerable<StockItemEvent> history)
        {
            Id = id;
            foreach (var item in history)
            {
                Apply(item, false);
            }
        }

        public void Create()
        {
            Apply(new StockItemCreatedEvent(
                Id,
                ProductType,
                Quantity,
                Price,
                Owner,
                AdvertId), isNewEvent: true);
        }

        public void Sell()
        {
            Apply(new StockItemSoldEvent(Id), isNewEvent: true);
        }

        public void Dispose()
        {
            Apply(new StockItemDisposedEvent(Id), isNewEvent: true);
        }

        private void Apply(StockItemEvent @event, bool isNewEvent)
        {
            ((dynamic)this).When((dynamic)@event);
            if (isNewEvent) _changes.Add(@event);
            History.Add(@event);
        }

        public Guid Id { get; private set; }
        public ProductType ProductType { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public StockOwner Owner { get; private set; }
        public Guid AdvertId { get; private set; }
        public IList<StockItemEvent> History { get; } = new List<StockItemEvent>();
        public StockItemStatus Status { get; set; }

        public IList<StockItemEvent> GetChanges()
        {
            return _changes;
        }

        private void When(StockItemCreatedEvent e)
        {
            Id = e.Id;
            ProductType = e.ProductType;
            Quantity = e.Quantity;
            Price = e.Price;
            Owner = e.StockOwner;
            AdvertId = e.AdvertId;
            Status = e.Status;
        }

        private void When(StockItemSoldEvent e)
        {
            Status = StockItemStatus.Sold;
            Quantity = 0;
        }

        private void When(StockItemDisposedEvent e)
        {
            Status = StockItemStatus.Disposed;
        }
    }
}