using System;

namespace GoFish.Advert
{
    public class PostAdvertCommand : ICommand<Advert>
    {
        public readonly Guid Id;

        public PostAdvertCommand(Guid id)
        {
            Id = id;
        }
    }
}