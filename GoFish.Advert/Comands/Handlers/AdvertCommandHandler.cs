using System;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public abstract class AdvertCommandHandler<T> : ICommandHandler<T, Advert>
        where T : ICommand<Advert>
    {
        protected readonly AdvertRepository Repository;
        private readonly IMessageBroker<Advert> _messageBroker;

        internal AdvertCommandHandler(AdvertRepository repository)
        {
            Repository = repository;
        }

        internal AdvertCommandHandler(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
        {
            Repository = repository;
            _messageBroker = messageBroker;
        }

        public abstract void Handle(T command);

        protected void Save(Advert advert)
        {
            Repository.Save(advert); // will save events rather than state soon!
        }

        protected void SendEventNotifications(Advert advert)
        {
            if (_messageBroker != null)
            {
                _messageBroker.SendMessagesFor(advert);
            }
            else
            {
                throw new InvalidOperationException("An Attempt was made to send messages with no instance of a Message Handler.");
            }
        }
    }
}