using GoFish.Shared.Dto;

namespace GoFish.Inventory
{
    public interface IStockItemFactory
    {
        StockItem BuildNew(StockItemDto fromDto);
        StockItem Update(StockItem StockItem, StockItemDto fromDto);
    }
}