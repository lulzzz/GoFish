using System.Text;
using AutoMapper;
using GoFish.Shared.Messaging;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

        public void Send(StockItem objectToSend)
        {
            var client = new MessagingClient(_logger, "172.17.0.1");
            var dto = _mapper.Map<StockItem, StockItemDto>(objectToSend);

            client.SendMessage("InventoryAdded", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
        }
    }
}