using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using GoFish.Shared.Dto;

namespace GoFish.Inventory.Receiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string QUEUE_NAME = "AdvertAdded";

            var factory = new ConnectionFactory();

            factory.HostName = "localhost";
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

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var payload = Encoding.UTF8.GetString(ea.Body);
                        var advert = JsonConvert.DeserializeObject<AdvertDto>(payload);

                        Console.WriteLine("Received Qty: {0}, Price: {1}, AdvertiserId: {2}",
                            advert.Quantity,
                            advert.Price,
                            advert.AdvertiserId
                        );

                        Console.WriteLine("Sending to InventoryApi");
                        // TODO: Call Inventory Api here
                    };

                    channel.BasicConsume(queue: QUEUE_NAME,
                                         noAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}