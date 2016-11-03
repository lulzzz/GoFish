using System;

namespace GoFish.Advert
{
    public class StockLevelChangedEvent : AdvertEvent
    {
        public readonly int StockLevel;
        public StockLevelChangedEvent(Guid id, int stockLevel) : base(id)
        {
            StockLevel = stockLevel;
        }
    }
}