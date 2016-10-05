using System;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class PostAdvertCommandHandler : AdvertCommandHandler<PostAdvertCommand>
    {
        public PostAdvertCommandHandler(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
            : base(repository, messageBroker) { }

        public override void Handle(PostAdvertCommand command)
        {
            // Get the advert
            var advert = Repository.Get(command.Id);

            // pre-Validate
            if (advert == null)
            {
                throw new AdvertNotFoundException($"Advert not found: {command.Id}");
            }

            // Act
            advert.Post();

            // Persist Events and Send Messages
            SaveEvents(advert);
            SendEventNotifications(advert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh all (this needs changing!)
            RefreshReadModel(advert);
        }
    }
}