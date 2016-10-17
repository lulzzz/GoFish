using System;

namespace GoFish.Advert
{
    public class PublishAdvertCommand : ICommand<Advert>
    {
        public readonly Guid Id;

        public PublishAdvertCommand(Guid id)
        {
            Id = id;
        }
    }
}