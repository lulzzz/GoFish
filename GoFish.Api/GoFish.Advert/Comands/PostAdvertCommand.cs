using System;

namespace GoFish.Advert
{
    public class PostAdvertCommand : UserCommand
    {
        public readonly Guid Id;

        public readonly bool AlsoPostToStock;

        public PostAdvertCommand(Guid id, int userId, bool alsoPostToStock) : base(userId)
        {
            Id = id;
            AlsoPostToStock = alsoPostToStock;
        }
    }
}