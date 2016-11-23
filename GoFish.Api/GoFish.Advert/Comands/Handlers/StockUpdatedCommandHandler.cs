using GoFish.Shared.Command;
using GoFish.Shared.Interface;

namespace GoFish.Advert
{
    public class StockUpdatedCommandHandler : AdvertCommandHandler<StockUpdatedCommand>
    {
        public StockUpdatedCommandHandler(AdvertRepository repository, IMessageBroker<Advert> messageBroker)
            : base(repository, messageBroker) { }

        public override void Handle(StockUpdatedCommand command)
        {
            // Get and check it
            var advert = Repository.Get(command.Id);

            if (advert == null)
                throw new ItemNotFoundException($"Advert not found: {command.Id}");

            // Do stuff
            advert.StockLevelChanged(command.StockLevel);

            SaveEvents(advert);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing
            RefreshReadModel(advert);

            SendEventNotifications(advert);
        }
    }
}