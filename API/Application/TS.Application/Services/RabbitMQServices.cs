using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TS.Application.Services
{
    public class RabbitMQServices : IRabbitMQServices
    {
        private readonly ConnectionFactory _connectionFactory;
        private const string queue_name = "Invoices";

        public RabbitMQServices()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
            };
        }

        public void Publisher(string message)
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

            var invoiceInfoJson = JsonSerializer.Serialize(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: queue_name,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(invoiceInfoJson)
            );
        }

        public string Consumer()
        {
            var message = string.Empty;

            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: queue_name,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message = Encoding.UTF8.GetString(body);
            };

            channel.BasicConsume(
                queue: queue_name,
                autoAck: true,
                consumer: consumer
            );

            return message;
        }
    }
}