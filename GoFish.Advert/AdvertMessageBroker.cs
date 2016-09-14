using AutoMapper;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GoFish.Advert
{
    public class AdvertMessageBroker : IMessageBroker<Advert>
    {
        // put these in appsettings.json file
        const string HOST_NAME = "localhost";
        const string QUEUE_NAME = "AdvertAdded";

        private readonly IMapper _mapper;

        public AdvertMessageBroker(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Send(Advert objectToSend)
        {
            var dto = _mapper.Map<Advert, AdvertDto>(objectToSend);
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
                // To Log
            }
        }
    }
}