using System;
using GoFish.Shared.Interface;
using GoFish.Shared.Command;

namespace GoFish.Inventory
{
    public class StockDisposedCommand : UserCommand, ICommand<StockItem>
    {
        public readonly Guid StockItemId;

        public StockDisposedCommand(Guid stockItemId, int userId) : base(userId)
        {
            StockItemId = stockItemId;
        }
    }
}