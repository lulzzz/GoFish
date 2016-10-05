using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace GoFish.Advert
{
    public class TestEventStore
    {
        public void Save(Guid id)
        {
            // Test Data
            var eventList = new List<AdvertEvent>();
            // eventList.Add(new AdvertCreatedEvent(id));
            eventList.Add(new AdvertPostedEvent(id));
            eventList.Add(new AdvertPublishedEvent(id));

            IEventStoreConnection conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            conn.ConnectAsync();

            foreach (var e in eventList)
            {
                var p = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e));
                var ev = new EventData(Guid.NewGuid(), e.GetType().Name, true, p, null);
                conn.AppendToStreamAsync($"Advert-{id}", ExpectedVersion.Any, ev).Wait();
            }
        }

        public Advert Get(Guid id)
        {
            var history = GetHistory(id);
            return new Advert(history);
        }

        private IEnumerable<AdvertEvent> GetHistory(Guid id)
        {
            var eventList = new List<AdvertEvent>();

            IEventStoreConnection conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            conn.ConnectAsync().Wait();

            var advertStream = conn.ReadStreamEventsForwardAsync($"Advert-{id}", StreamPosition.Start, 999, false).Result;

            foreach (var e in advertStream.Events)
            {
                var d = Encoding.UTF8.GetString(e.Event.Data);
                if (e.Event.EventType == "AdvertCreatedEvent")
                {
                    var d2 = JsonConvert.DeserializeObject<AdvertCreatedEvent>(d);
                    eventList.Add(d2);
                }
            }

            return eventList;
        }
    }
}