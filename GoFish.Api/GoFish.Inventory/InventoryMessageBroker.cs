using System.Text;
using AutoMapper;
using GoFish.Shared.Messaging;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GoFish.Inventory
{
    public class InventoryMessageBroker : IMessageBroker<StockItem>
    {
        private readonly IMapper _mapper;
        private ILogger<InventoryMessageBroker> _logger;

        public InventoryMessageBroker(ILogger<InventoryMessageBroker> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public void SendMessagesFor(StockItem objectToSend)
        {
            var client = new MessagingClient(_logger, "172.17.0.1");

            var dto = _mapper.Map<StockItem, StockItemDto>(objectToSend);

            // whitelist of events that have associated MQ messages sent
            var whiteList = new List<string>() {
                "StockItemCreatedEvent",
                "StockItemSoldEvent"
            };

            var whiteListed = objectToSend.GetChanges()
                .Where(m => whiteList.Contains(m.GetType().Name));

            foreach (var message in whiteListed)
            {
                client.SendMessage(message.GetType().ToString().Replace("Event", string.Empty), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
            }
        }
    }
}