using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace GoFish.Shared.Messaging
{
    public class MessagingClient
    {
        private readonly ILogger _logger;

        public MessagingClient(ILogger logger)
        {
            _logger = logger;
        }

        public MessagingClient(ILogger logger, string hostName) : this(logger)
        {
            HostName = hostName;
        }

        public MessagingClient(ILogger logger, string hostName, int port) : this(logger, hostName)
        {
            Port = port;
        }

        public string HostName { get; set; } = "172.17.0.1";
        public int Port { get; set; } = 5672;

        public void SendMessage(string queueName, byte[] payload)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = HostName,
                    Port = Port,
                    UserName = "gofish",
                    Password = "gofish",
                    VirtualHost = "/"
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: queueName,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);


                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        channel.BasicPublish(exchange: "",
                                             routingKey: queueName,
                                             basicProperties: properties,
                                             body: payload);

                    }
                }
            }
            catch (System.Exception)
            {
                _logger.LogError("Error sending the Advert message to Inventroy via the {0} queue", queueName);
            }
        }
    }
}
