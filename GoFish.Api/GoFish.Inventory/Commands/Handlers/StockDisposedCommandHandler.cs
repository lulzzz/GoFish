using GoFish.Shared.Command;

namespace GoFish.Inventory
{
    public class StockDisposedCommandHandler : StockItemCommandHandler<StockDisposedCommand>
    {
        private readonly IStockItemFactory _factory;

        public StockDisposedCommandHandler(
            InventoryRepository repository,
            IStockItemFactory factory
            ) : base(repository)
        {
            _factory = factory;
        }

        public override void Handle(StockDisposedCommand command)
        {
            // construct an advert
            var stockItem = Repository.Get(command.StockItemId);

            if (stockItem.Owner.Id != command.UserId)
                throw new ItemNotOwnedException($"Advert not yours: {command.StockItemId} requested: {stockItem.Owner.Id} is: {command.UserId}");

            // Act
            stockItem.Dispose();

            // Save resulting events
            SaveEvents(stockItem);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing.
            RefreshReadModel(stockItem);
        }
    }
}