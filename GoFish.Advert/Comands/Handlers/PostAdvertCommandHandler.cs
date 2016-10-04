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

            if (advert.Status != AdvertStatus.Created)
            {
                throw new InvalidOperationException("Can only post non-posted & non-published adverts.");
            }

            // Act
            advert.Post();

            // TODO: Post Validate?  or throw exceptions from .Post() ?

            // Persist Events and Send Messages
            Save(advert);
            SendEventNotifications(advert);
        }
    }
}