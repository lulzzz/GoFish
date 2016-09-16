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
            const string QUEUE_NAME = "InventoryAdded";

            try
            {
                // login details need securing
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

                            Console.WriteLine("Message received: Inventory added");
                            Console.WriteLine("Sending to Advert API");
                            // TODO: Call Advert Api here to set advert status to "Public".
                        };

                        channel.BasicConsume(queue: QUEUE_NAME,
                                             noAck: true,
                                             consumer: consumer);

                        Console.WriteLine("Advert Receiver listening.  Press [enter] to exit.");
                        Console.ReadLine();
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error running the Inventory Receiver message queue");
            }
        }
    }
}