using System;

namespace GoFish.Inventory
{
    public abstract class StockItemEvent
    {
        public readonly Guid Id;

        public StockItemEvent(Guid id)
        {
            Id = id;
        }
    }
}