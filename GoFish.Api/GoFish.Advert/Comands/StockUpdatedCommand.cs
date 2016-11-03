using System;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class StockUpdatedCommand : ICommand<Advert>
    {
        public readonly Guid Id;
        public readonly int StockLevel;

        public StockUpdatedCommand(Guid id, int stockLevel)
        {
            Id = id;
            StockLevel = stockLevel;
        }
    }
}