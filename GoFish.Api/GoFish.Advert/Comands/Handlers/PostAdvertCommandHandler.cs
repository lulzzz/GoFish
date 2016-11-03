using GoFish.Shared.Command;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class PostAdvertCommandHandler : AdvertCommandHandler<PostAdvertCommand>
    {
        public PostAdvertCommandHandler(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
            : base(repository, messageBroker) { }

        public override void Handle(PostAdvertCommand command)
        {
            // Get and check it
            var advert = Repository.Get(command.Id);

            if (advert == null)
                throw new ItemNotFoundException($"Advert not found: {command.Id}");

            if (advert.Advertiser.Id != command.UserId)
                throw new ItemNotFoundException($"Advert not found: {command.Id}");

            // Do stuff
            advert.Post();

            if (command.AlsoPostToStock)
            {
                advert.PostToStock(command.StockQuantity);
            }
            else
            {
                // TODO: this will get handled out of process eventually, but
                // at the moment, there is no receiver for the "just publish" message
                // so set it to published here for now
                advert.Publish(command.StockQuantity);
            }


            SaveEvents(advert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing
            RefreshReadModel(advert);

            SendEventNotifications(advert);
        }
    }
}