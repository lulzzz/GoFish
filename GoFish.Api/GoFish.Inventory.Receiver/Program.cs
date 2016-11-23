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
            const string QUEUE_NAME = "GoFish.Advert.AdvertPostedToStock";

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
                        Console.WriteLine($"Inventory Receiver message queue listening for {QUEUE_NAME}");

                        channel.QueueDeclare(queue: QUEUE_NAME,
                                                durable: true,
                                                exclusive: false,
                                                autoDelete: false,
                                                arguments: null);


                        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                        var consumer = new EventingBasicConsumer(channel);

                        consumer.Received += (model, ea) =>
                        {
                            var payload = Encoding.UTF8.GetString(ea.Body);
                            var advert = JsonConvert.DeserializeObject<AddAdvertToStockDto>(payload);

                            var api = new ApiProxy();
                            api.UpdateInventory(new StockItemDto()
                            {
                                Id = Guid.NewGuid(),
                                ProductTypeId = (int)advert.CatchTypeId,
                                Quantity = (int)advert.StockQuantity,
                                Price = (double)advert.Price,
                                OwnerId = (int)advert.AdvertiserId,
                                AdvertId = advert.Id
                            });

                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        };

                        channel.BasicConsume(queue: QUEUE_NAME,
                                             noAck: false,
                                             consumer: consumer);

                        channel.BasicConsume(queue: QUEUE_NAME, noAck: true, consumer: consumer);
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