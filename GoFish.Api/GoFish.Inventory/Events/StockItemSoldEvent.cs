using System;

namespace GoFish.Inventory
{
    public class StockItemSoldEvent : StockItemEvent
    {
        public StockItemSoldEvent(Guid id) : base(id) { }
    }
}