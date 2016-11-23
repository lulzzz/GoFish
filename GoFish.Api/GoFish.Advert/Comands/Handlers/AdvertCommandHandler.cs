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

        protected void SaveEvents(Advert advert)
        {
            Repository.Save(advert);
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

        protected void RefreshReadModel(Advert advert)
        {
            foreach (var item in advert.GetChanges())
            {
                if (item.GetType().Name == "AdvertCreatedEvent")
                {
                    Repository.SaveCreatedAdvert(advert);
                }
                if (item.GetType().Name == "AdvertUpdatedEvent"
                || item.GetType().Name == "StockLevelChangedEvent")
                {
                    Repository.UpdateAdvert(advert);
                }
                if (item.GetType().Name == "AdvertPostedEvent"
                || item.GetType().Name == "AdvertPublishedEvent"
                || item.GetType().Name == "AdvertWithdrawnEvent")
                {
                    Repository.DeleteAdvert(advert);
                    Repository.SaveAdvert(advert);
                }
            }
        }
    }
}