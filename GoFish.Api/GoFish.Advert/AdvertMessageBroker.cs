using System.Text;
using AutoMapper;
using GoFish.Shared.Messaging;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace GoFish.Advert
{
    public class AdvertMessageBroker : IMessageBroker<Advert>
    {
        private readonly IMapper _mapper;
        private ILogger<AdvertMessageBroker> _logger;

        public AdvertMessageBroker(ILogger<AdvertMessageBroker> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public void SendMessagesFor(Advert objectWithEventsToBroadcast)
        {
            var client = new MessagingClient(_logger, "172.17.0.1");

            var dto = _mapper.Map<Advert, AdvertDto>(objectWithEventsToBroadcast);

            // whitelist of events that have associated MQ messages sent
            var whiteList = new List<string>() {
                "AdvertPostedEvent",
                "AdvertPostedToStockEvent",
                "AdvertPublishedEvent"
            };

            var whiteListed = objectWithEventsToBroadcast.GetChanges()
                .Where(m => whiteList.Contains(m.GetType().Name));

            foreach (var message in whiteListed)
            {
                client.SendMessage(message.GetType().ToString().Replace("Event", string.Empty), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
            }
        }
    }
}