using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class PostAdvertCommandHandler : AdvertCommandHandler<PostAdvertCommand>
    {
        public PostAdvertCommandHandler(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
            : base(repository, messageBroker) { }

        public override void Handle(PostAdvertCommand command)
        {
            var advert = Repository.Get(command.Id);

            if (advert == null)
            {
                throw new AdvertNotFoundException($"Advert not found: {command.Id}");
            }

            // Do it!
            advert.Post();

            SaveEvents(advert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing
            RefreshReadModel(advert);

            SendEventNotifications(advert);
        }
    }
}