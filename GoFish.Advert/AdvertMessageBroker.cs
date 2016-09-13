using AutoMapper;
using GoFish.Shared.Dto;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GoFish.Advert
{
    public interface IMessageBroker<T>
    {
        void Send(T objectToSend);
    }

    public class AdvertMessageBroker : IMessageBroker<Advert>
    {
        const string QUEUE_NAME = "advert_to_inventory";

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
                var factory = new ConnectionFactory { HostName = "localhost" };

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