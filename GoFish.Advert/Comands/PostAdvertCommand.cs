using System;

namespace GoFish.Advert
{
    public class PostAdvertCommand : ICommand<Advert>
    {
        public PostAdvertCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}