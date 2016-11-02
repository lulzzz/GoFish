using System;
using GoFish.Shared.Interface;

namespace GoFish.Inventory
{
    public abstract class StockItemCommandHandler<T> : ICommandHandler<T, StockItem>
        where T : ICommand<StockItem>
    {
        protected readonly InventoryRepository Repository;
        private readonly IMessageBroker<StockItem> _messageBroker;

        internal StockItemCommandHandler(InventoryRepository repository)
        {
            Repository = repository;
        }

        internal StockItemCommandHandler(InventoryRepository repository, IMessageBroker<StockItem> messageBroker)
        {
            Repository = repository;
            _messageBroker = messageBroker;
        }

        public abstract void Handle(T command);

        protected void SaveEvents(StockItem StockItem)
        {
            Repository.Save(StockItem);
        }

        protected void SendEventNotifications(StockItem StockItem)
        {
            if (_messageBroker != null)
            {
                _messageBroker.SendMessagesFor(StockItem);
            }
            else
            {
                throw new InvalidOperationException("An Attempt was made to send messages with no instance of a Message Handler.");
            }
        }

        protected void RefreshReadModel(StockItem StockItem)
        {
            foreach (var item in StockItem.GetChanges())
            {
                if (item.GetType().Name == "StockItemCreatedEvent")
                {
                    Repository.SaveCreatedStockItem(StockItem);
                }
                if (item.GetType().Name == "StockItemSoldEvent" || item.GetType().Name == "StockItemDisposedEvent")
                {
                    Repository.DeleteStockItem(StockItem);
                }
            }
        }
    }
}