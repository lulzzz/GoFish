using System;
using GoFish.Shared.Interface;
using GoFish.Shared.Command;

namespace GoFish.Inventory
{
    public class StockSoldCommand : UserCommand, ICommand<StockItem>
    {
        public readonly Guid StockItemId;

        public StockSoldCommand(Guid stockItemId, int userId) : base(userId)
        {
            StockItemId = stockItemId;
        }
    }
}