using System;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class PublishAdvertCommandHandler : AdvertCommandHandler<PublishAdvertCommand>
    {
        public PublishAdvertCommandHandler(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
            : base(repository, messageBroker) { }

        public override void Handle(PublishAdvertCommand command)
        {
            var advert = Repository.Get(command.Id);

            if (advert == null)
                throw new AdvertNotFoundException($"Advert not found: {command.Id}");

            if (advert.Status != AdvertStatus.Posted)
                throw new InvalidOperationException("Can only publish adverts in the posted status.");

            // Do it!
            advert.Publish();

            SaveEvents(advert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh all (this needs changing!)
            RefreshReadModel(advert);

            SendEventNotifications(advert);
        }
    }
}