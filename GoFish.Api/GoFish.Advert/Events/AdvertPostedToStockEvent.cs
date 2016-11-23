using System;

namespace GoFish.Advert
{
    public class AdvertPostedToStockEvent : AdvertEvent
    {
        public readonly int StockQuantity;
        public AdvertPostedToStockEvent(Guid id, int stockQuantity) : base(id)
        {
            StockQuantity = stockQuantity;
        }
    }
}