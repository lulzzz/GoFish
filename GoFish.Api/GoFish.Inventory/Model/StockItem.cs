using System;

namespace GoFish.Inventory
{
    public class StockItem
    {
        private StockItem () { }

        public StockItem (Guid id, ProductType productType, int quantity, double price, StockOwner owner, Guid advertId)
        {
            Id = id;
            ProductType = productType;
            Quantity = quantity;
            Price = price;
            Owner = owner;
            AdvertId = advertId;
        }

        public Guid Id { get; private set; }
        public ProductType ProductType { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public StockOwner Owner { get; private set; }
        public Guid AdvertId { get; private set; }
    }
}