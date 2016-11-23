using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using GoFish.Shared.Dto;

namespace GoFish.Advert.StockReceiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string HOST_NAME = "172.17.0.1";
            const string QUEUE_NAME = "GoFish.Inventory.StockItemSold";

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

                        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var payload = Encoding.UTF8.GetString(ea.Body);
                            var stockItem = JsonConvert.DeserializeObject<StockItemDto>(payload);

                            var api = new ApiProxy();
                            api.UpdateAdvert(stockItem);

                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        };

                        channel.BasicConsume(queue: QUEUE_NAME,
                                             noAck: false,
                                             consumer: consumer);

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