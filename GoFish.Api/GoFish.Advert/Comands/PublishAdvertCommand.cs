using System;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class PublishAdvertCommand : ICommand<Advert>
    {
        public readonly Guid Id;
        public readonly int StockAdded;

        public PublishAdvertCommand(Guid id, int stockAdded)
        {
            Id = id;
            StockAdded = stockAdded;
        }
    }
}