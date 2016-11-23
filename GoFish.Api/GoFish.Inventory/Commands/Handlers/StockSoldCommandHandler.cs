using GoFish.Shared.Command;
using GoFish.Shared.Interface;

namespace GoFish.Inventory
{
    public class StockSoldCommandHandler : StockItemCommandHandler<StockSoldCommand>
    {
        private readonly IStockItemFactory _factory;

        public StockSoldCommandHandler(
            InventoryRepository repository,
            IStockItemFactory factory,
            IMessageBroker<StockItem> messageBroker
            ) : base(repository, messageBroker)
        {
            _factory = factory;

        }

        public override void Handle(StockSoldCommand command)
        {
            // construct an advert
            var stockItem = Repository.Get(command.StockItemId);

            if (stockItem.Owner.Id != command.UserId)
                throw new ItemNotOwnedException($"Advert not yours: {command.StockItemId} requested: {stockItem.Owner.Id} is: {command.UserId}");

            // Act
            stockItem.Sell();

            // Save resulting events
            SaveEvents(stockItem);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing.
            RefreshReadModel(stockItem);

            SendEventNotifications(stockItem);
        }
    }
}