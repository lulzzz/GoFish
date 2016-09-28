using System;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class PublishAdvertCommandHandler : ICommandHandler<PublishAdvertCommand, Advert>
    {
        private readonly AdvertRepository _repository;
        private readonly IMessageBroker<Advert> _messageBroker;

        public PublishAdvertCommandHandler(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public Advert Handle(PublishAdvertCommand command)
        {
            var advert = _repository.Get(command.Id);

            if (advert == null)
            {
                throw new AdvertNotFoundException($"Advert {command.Id} not found.");
            }

            if (advert.Status != AdvertStatus.Posted)
            {
                throw new InvalidOperationException("Can only publish posted adverts.");
            }

            advert.Publish();

            _repository.Save(advert);

            _messageBroker.Send("AdvertPublished", advert);

            return advert;
        }
    }
}