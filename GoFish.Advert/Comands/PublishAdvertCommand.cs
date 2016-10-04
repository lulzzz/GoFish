using System;

namespace GoFish.Advert
{
    public class PublishAdvertCommand : ICommand<Advert>
    {
        public PublishAdvertCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}