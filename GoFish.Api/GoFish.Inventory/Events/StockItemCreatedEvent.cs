using System;

namespace GoFish.Inventory
{
    public class StockItemCreatedEvent : StockItemEvent
    {
        public StockItemCreatedEvent(
            Guid id,
            ProductType productType,
            int quantity,
            double price,
            StockOwner owner,
            Guid advertId
            ) : base(id)
        {
            ProductType = productType;
            Quantity = quantity;
            Price = price;
            StockOwner = owner;
            AdvertId = advertId;
            Status = StockItemStatus.ForSale;
        }

        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public StockOwner StockOwner { get; set; }
        public Guid AdvertId { get; set; }
        public StockItemStatus Status { get; set; }
    }
}