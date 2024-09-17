using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace TS.Application.Services
{
    public class RabbitMQServices : IRabbitMQServices
    {
        private readonly ConnectionFactory _connectionFactory;
        private const string queue_name = "invoices";

        public RabbitMQServices()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
            };
        }

        public void Publisher(MessageQueue message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: queue_name,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            channel.BasicPublish(
                exchange: "",
                routingKey: queue_name,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message))
            );
        }
    }

    public class MessageQueue
    {
        public int TypeFile { get; set; }
        public long InvoiceId { get; set; }
    }
}