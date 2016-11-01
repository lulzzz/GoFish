using GoFish.Shared.Command;

namespace GoFish.Inventory
{
    public class CreateStockItemCommandHandler : StockItemCommandHandler<CreateStockItemCommand>
    {
        private readonly IStockItemFactory _factory;

        public CreateStockItemCommandHandler(InventoryRepository repository, IStockItemFactory factory) : base(repository)
        {
            _factory = factory;
        }

        public override void Handle(CreateStockItemCommand command)
        {
            // construct an advert
            var stockItem = _factory.BuildNew(command.StockItem);

            if(stockItem.Owner.Id != command.UserId)
                throw new ItemNotOwnedException($"Advert not yours: {command.Id}");

            // Act
            stockItem.Create();

            // Save resulting events
            SaveEvents(stockItem);

            // TODO: This can be done out of process by responding to the events/messages
            // For now, the simplest thing is to refresh here but this needs changing.
            RefreshReadModel(stockItem);
        }
    }
}