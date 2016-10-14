using System;

namespace GoFish.Advert
{
    public class PublishAdvertCommand : UserCommand
    {
        public readonly Guid Id;

        public PublishAdvertCommand(Guid id, int userId) : base(userId)
        {
            Id = id;
        }
    }
}