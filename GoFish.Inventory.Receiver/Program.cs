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

            const string HOST_NAME = "172.17.0.1";

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

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var payload = Encoding.UTF8.GetString(ea.Body);
                            var advert = JsonConvert.DeserializeObject<AdvertDto>(payload);

                            Console.WriteLine("Received CatchTypeId {0}, Qty: {1}, Price: {2}, AdvertiserId: {3}, AdvertId {4}",
                                advert.CatchTypeId,
                                advert.Quantity,
                                advert.Price,
                                advert.AdvertiserId,
                                advert.AdvertId
                            );

                            Console.WriteLine("Sending to InventoryApi");

                            var api = new ApiProxy();
                            api.UpdateInventory(new StockItemDto()
                            {
                                ProductTypeId = advert.CatchTypeId,
                                Quantity = advert.Quantity,
                                Price = advert.Price,
                                OwnerId = advert.AdvertiserId,
                                AdvertId = advert.AdvertId
                            });

                            Console.WriteLine("Sent to InventoryApi");
                        };

                        channel.BasicConsume(queue: QUEUE_NAME, noAck: true, consumer: consumer);

                        Console.WriteLine("Inventory Receiver listening.  Press [enter] to exit.");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error running the Inventory Receiver message queue");
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}