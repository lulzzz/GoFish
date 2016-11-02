using System;
using System.Collections.Generic;
using System.Text;
using EventStore.ClientAPI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GoFish.Inventory
{
    public class InventoryRepository
    {
        private readonly InventoryDbContext _readModel;
        private readonly IEventStoreConnection _writeModel;

        public InventoryRepository(InventoryDbContext readModel, IEventStoreConnection writeModel)
        {
            _readModel = readModel;
            _writeModel = writeModel;
            _writeModel.ConnectAsync();
        }

        internal StockItem Get(Guid id)
        {
            var stockStream =
                _writeModel.ReadStreamEventsForwardAsync(
                    $"Stock-{id}",
                    StreamPosition.Start,
                    999, // TODO: Load batches or implement snapshots - this is fine for the prototype
                    false
                ).Result;

            if (stockStream.Status != SliceReadStatus.Success)
                return null; // TODO: Consider NULL object pattern

            var eventList = new List<StockItemEvent>();

            foreach (var e in stockStream.Events)
            {
                // TODO: reflect this little lot
                var d = Encoding.UTF8.GetString(e.Event.Data);
                if (e.Event.EventType == "StockItemCreatedEvent")
                {
                    var d2 = JsonConvert.DeserializeObject<StockItemCreatedEvent>(d);
                    eventList.Add(d2);
                }
            }

            return new StockItem(id, eventList);
        }

        internal void SaveCreatedStockItem(StockItem StockItem)
        {
            _readModel.StockItems.Add(StockItem);
            _readModel.StockOwners.Attach(StockItem.Owner);
            _readModel.ProductTypes.Attach(StockItem.ProductType);
            _readModel.SaveChanges();
        }

        internal void UpdateStockItem(StockItem StockItem)
        {
            // _readModel.StockItems.Attach(StockItem);
            // _readModel.Entry(StockItem).State = EntityState.Modified;
            // _readModel.SaveChanges();
        }

        internal void SaveStockItem(StockItem StockItem)
        {
            // _readModel.StockItems.Add(StockItem);
            // _readModel.SaveChanges();
        }

        internal void DeleteStockItem(StockItem StockItem)
        {
            _readModel.StockItems.Attach(StockItem);
            _readModel.Entry(StockItem).State = EntityState.Deleted;
            _readModel.SaveChanges();
        }

        internal void Save(StockItem item)
        {
            foreach (var e in item.GetChanges())
            {
                var p = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e));
                var ev = new EventData(Guid.NewGuid(), e.GetType().Name, true, p, null);
                _writeModel.AppendToStreamAsync($"Stock-{item.Id}", ExpectedVersion.Any, ev).Wait();
            }
        }
    }
}