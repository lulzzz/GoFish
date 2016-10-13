using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            var advertStream =
                _writeModel.ReadStreamEventsForwardAsync(
                    $"Advert-{id}",
                    StreamPosition.Start,
                    999, // TODO: Load batches or implement snapshots - this is fine for the prototype
                    false
                ).Result;

            if (advertStream.Status != SliceReadStatus.Success)
                return null; // TODO: Consider NULL object pattern

            var eventList = new List<AdvertEvent>();

            foreach (var e in advertStream.Events)
            {
                // TODO: reflect this little lot
                var d = Encoding.UTF8.GetString(e.Event.Data);
                if (e.Event.EventType == "AdvertCreatedEvent")
                {
                    var d2 = JsonConvert.DeserializeObject<AdvertCreatedEvent>(d);
                    eventList.Add(d2);
                }
                if (e.Event.EventType == "AdvertUpdatedEvent")
                {
                    var d2 = JsonConvert.DeserializeObject<AdvertUpdatedEvent>(d);
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

            return new Advert(id, eventList);
        }

        internal void SaveCreatedAdvert(Advert advert)
        {
            _readModel.Adverts.Add(advert);
            _readModel.Advertisers.Attach(advert.Advertiser);
            _readModel.CatchTypes.Attach(advert.CatchType);
            _readModel.SaveChanges();
        }

        internal void UpdateAdvert(Advert advert)
        {
            _readModel.Adverts.Attach(advert);
            _readModel.Entry(advert).State = EntityState.Modified;
            _readModel.SaveChanges();
        }

        internal void SaveAdvert(Advert advert)
        {
            _readModel.Adverts.Add(advert);
            _readModel.SaveChanges();
        }

        internal void DeleteAdvert(Advert advert)
        {
            _readModel.Adverts.Attach(advert);
            _readModel.Entry(advert).State = EntityState.Deleted;
            _readModel.SaveChanges();
        }

        internal IEnumerable<Advert> GetDraftAdverts(int userId)
        {
            return _readModel.Adverts
                .Include(ct => ct.CatchType)
                .Include(a => a.Advertiser)
                .Where(s => s.Status == AdvertStatus.Created)
                .Where(u => u.Advertiser.Id == userId);
        }

        internal IEnumerable<Advert> GetPublished(int userId)
        {
            return _readModel.Adverts
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Published)
                .Where(u => u.Advertiser.Id == userId);
        }

        internal object GetPosted(int userId)
        {
            return _readModel.Adverts
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Posted)
                .Where(u => u.Advertiser.Id == userId);
        }

        public object GetWithdrawn(int userId)
        {
            return _readModel.Adverts
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .Where(s => s.Status == AdvertStatus.Withdrawn)
                .Where(u => u.Advertiser.Id == userId);
        }

        internal IEnumerable<Advert> Search(int userId, AdvertSearchOptions options)
        {
            if (options.Status?.ToLower() != "active" && options.Status?.ToLower() != "inactive")
                throw new ArgumentOutOfRangeException("Status");

            Func<Advert, bool> filter;

            if (options.Status == "Active")
            {
                filter = a => a.Status == AdvertStatus.Created
                    || a.Status == AdvertStatus.Posted
                    || a.Status == AdvertStatus.Published;
            }
            else
            {
                filter = a => a.Status == AdvertStatus.Withdrawn
                    || a.Status == AdvertStatus.Unknown;
            }

            return _readModel.Adverts
                .Include(a => a.Advertiser)
                .Include(ct => ct.CatchType)
                .Where(u => u.Advertiser.Id == userId)
                .Where(filter);
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

    public class AdvertSearchOptions
    {
        [Required(AllowEmptyStrings = false)]
        public string Status { get; set; }
    }
}