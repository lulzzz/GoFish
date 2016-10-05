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
            const string HOST_NAME = "172.17.0.1";
            const string QUEUE_NAME = "GoFish.Advert.AdvertPosted";

            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = HOST_NAME,
                    Port = 5672,
                    UserName = "gofish",
                    Password = "gofish",
                    VirtualHost = "/"
                };

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

                            var api = new ApiProxy();
                            api.UpdateInventory(new StockItemDto()
                            {
                                ProductTypeId = advert.CatchTypeId,
                                Quantity = advert.Quantity,
                                Price = advert.Price,
                                OwnerId = advert.AdvertiserId,
                                AdvertId = advert.Id
                            });
                        };

                        channel.BasicConsume(queue: QUEUE_NAME, noAck: true, consumer: consumer);
                        System.Console.WriteLine("Started");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error running the Inventory Receiver message queue");
                Console.WriteLine(ex.Message);
            }
        }
    }
}