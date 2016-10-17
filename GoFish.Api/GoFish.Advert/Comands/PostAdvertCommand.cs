using System;

namespace GoFish.Advert
{
    public class PostAdvertCommand : UserCommand
    {
        public readonly Guid Id;

        public PostAdvertCommand(Guid id, int userId) : base(userId)
        {
            Id = id;
        }
    }
}