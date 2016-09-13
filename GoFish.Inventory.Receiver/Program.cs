using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using GoFish.Shared.Dto;

namespace gofish.inventory.receiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string QUEUE_NAME = "advert_to_inventory";

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUE_NAME,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var payload = Encoding.UTF8.GetString(ea.Body);
                        var advert = JsonConvert.DeserializeObject<AdvertDto>(payload);

                        Console.WriteLine(" [x] Received Qty: {0}, Price: {1}, AdvertiserId: {2}",
                            advert.Quantity,
                            advert.Price,
                            advert.AdvertiserId
                        );
                    };

                    channel.BasicConsume(queue: QUEUE_NAME,
                                         noAck: true,
                                         consumer: consumer);

                    Console.ReadLine();
                }
            }
        }
    }
}