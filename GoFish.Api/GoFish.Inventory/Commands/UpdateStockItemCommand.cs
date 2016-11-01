using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using GoFish.Shared.Command;

namespace GoFish.Inventory
{
    public class UpdateStockItemCommand : UserCommand, ICommand<StockItem>
    {

        public UpdateStockItemCommand(StockItemDto stockItem, int userId) : base(userId)
        {
            StockItem = stockItem;
        }

        public StockItemDto StockItem { get; private set; }
    }
}