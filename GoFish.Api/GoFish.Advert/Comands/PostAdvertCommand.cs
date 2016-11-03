using System;
using GoFish.Shared.Command;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class PostAdvertCommand : UserCommand, ICommand<Advert>
    {
        public readonly Guid Id;
        public readonly bool AlsoPostToStock;
        public readonly int StockQuantity;

        public PostAdvertCommand(Guid id, int userId, bool alsoPostToStock, int stockQuantity) : base(userId)
        {
            Id = id;
            AlsoPostToStock = alsoPostToStock;
            StockQuantity = stockQuantity;
        }
    }
}