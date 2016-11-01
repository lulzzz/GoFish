using System;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using GoFish.Shared.Command;

namespace GoFish.Inventory
{
    public class CreateStockItemCommand : UserCommand, ICommand<StockItem>
    {
        public Guid Id
        {
            get
            {
                return StockItem.Id;
            }
        }

        public readonly StockItemDto StockItem;

        public CreateStockItemCommand(StockItemDto stockItem, int userId) : base(userId)
        {
            StockItem = stockItem;
        }
    }
}