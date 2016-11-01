using System;
using GoFish.Shared.Dto;

namespace GoFish.Inventory
{
    public class StockItemFactory : IStockItemFactory
    {
        public StockItem BuildNew(StockItemDto buildData)
        {
            ValidateInput(buildData);

            var resultingStockItem = new StockItem(
                buildData.Id,
                LookupItem.GetFromCache<ProductType>((int)buildData.ProductTypeId),
                (int)buildData.Quantity,
                (double)buildData.Price,
                LookupItem.GetFromCache<StockOwner>((int)buildData.OwnerId),
                buildData.AdvertId);

            return resultingStockItem;
        }

        public StockItem Update(StockItem StockItem, StockItemDto fromDto)
        {
            var resultingStockItem = BuildNew(fromDto);
            // resultingStockItem.Update();
            return resultingStockItem;
        }

        private static void ValidateInput(StockItemDto fromDto)
        {
            if (fromDto.ProductTypeId == null)
                throw new ArgumentNullException("Product Type");
        }
    }
}