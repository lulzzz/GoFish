using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventStore.ClientAPI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GoFish.Advert
{
    public class AdvertRepository
    {
        private readonly AdvertisingDbContext _readModel;
        private readonly IEventStoreConnection _writeModel;

        public AdvertRepository(AdvertisingDbContext readModel, IEventStoreConnection writeModel)
        {
            _readModel = readModel;
            _writeModel = writeModel;
            _writeModel.ConnectAsync();
        }

        internal Advert Get(Guid id)
        {
            var advertStream = _writeModel.ReadStreamEventsForwardAsync(
                    $"Advert-{id}",
                    StreamPosition.Start,
                    999, // TODO: Load batches or implement snapshots
                    false
                ).Result;

            var eventList = new List<AdvertEvent>();
            foreach (var e in advertStream.Events)
            {
                var d = Encoding.UTF8.GetString(e.Event.Data);
                if (e.Event.EventType == "AdvertCreatedEvent")
                {
                    var d2 = JsonConvert.DeserializeObject<AdvertCreatedEvent>(d);
                    eventList.Add(d2);
                }
                if (e.Event.EventType == "AdvertPostedEvent")
                {
                    var d2 = JsonConvert.DeserializeObject<AdvertPostedEvent>(d);
                    eventList.Add(d2);
                }
                if (e.Event.EventType == "AdvertPublishedEvent")
                {
                    var d2 = JsonConvert.DeserializeObject<AdvertPublishedEvent>(d);
                    eventList.Add(d2);
                }
            }

            return eventList.Count == 0 ? null : new Advert(eventList); // TODO: Consider NULL Object Pattern
        }

        internal void SaveCreatedAdvert(Advert advert)
        {
            _readModel.Adverts.Add(advert);
            _readModel.Advertisers.Attach(advert.Advertiser);
            _readModel.CatchTypes.Attach(advert.CatchType);
            _readModel.SaveChanges();
        }

        internal IEnumerable<Advert> GetDraftAdverts()
        {
            return _readModel.Adverts
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Created);
        }

        internal void SavePostedAdvert(Advert advert)
        {
            _readModel.Adverts.Add(advert);
            _readModel.SaveChanges();
        }

        internal void DeletePostedAdvert(Advert advert)
        {
            _readModel.Adverts.Attach(advert);
            _readModel.Entry(advert).State = EntityState.Deleted;
            _readModel.SaveChanges();
        }

        internal void SavePublishedAdvert(Advert advert)
        {
            _readModel.Adverts.Add(advert);
            _readModel.SaveChanges();
        }

        internal void DeleteCreatedAdvert(Advert advert)
        {
            _readModel.Adverts.Attach(advert);
            _readModel.Entry(advert).State = EntityState.Deleted;
            _readModel.SaveChanges();
        }

        internal IEnumerable<Advert> GetPublished()
        {
            return _readModel.Adverts
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Created);
        }

        internal object GetPosted()
        {
            return _readModel.Adverts
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Created);
        }

        internal void Save(Advert item)
        {
            foreach (var e in item.GetChanges())
            {
                var p = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e));
                var ev = new EventData(Guid.NewGuid(), e.GetType().Name, true, p, null);
                _writeModel.AppendToStreamAsync($"Advert-{item.Id}", ExpectedVersion.Any, ev).Wait();
            }
        }
    }
}