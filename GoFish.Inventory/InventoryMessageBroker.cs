using AutoMapper;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace GoFish.Inventory
{
    public class InventoryMessageBroker : IMessageBroker<StockItem>
    {
        const string HOST_NAME = "localhost";
        const string QUEUE_NAME = "InventoryAdded";

        private ILogger<InventoryMessageBroker> _logger;
        private readonly IMapper _mapper;

        public InventoryMessageBroker(ILogger<InventoryMessageBroker> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public void Send(StockItem objectToSend)
        {
            var dto = _mapper.Map<StockItem, StockItemDto>(objectToSend);
            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));
            SendMessage(payload);
        }

        private void SendMessage(byte[] payload)
        {
            try
            {
                // login details need securing
                var factory = new ConnectionFactory();

                factory.HostName = HOST_NAME;
                factory.Port = 5672;
                factory.UserName = "gofish";
                factory.Password = "gofish";
                factory.VirtualHost = "/";

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: QUEUE_NAME,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        channel.BasicPublish(exchange: "",
                                             routingKey: QUEUE_NAME,
                                             basicProperties: properties,
                                             body: payload);
                    }
                }
            }
            catch (System.Exception)
            {
                _logger.LogError("Error utilising the {0} message queue", QUEUE_NAME);
            }
        }
    }
}