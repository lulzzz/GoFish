using System;

namespace GoFish.Inventory
{
    public class StockItemDisposedEvent : StockItemEvent
    {
        public StockItemDisposedEvent(Guid id) : base(id) { }
    }
}