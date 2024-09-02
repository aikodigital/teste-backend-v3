using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using IModel = RabbitMQ.Client.IModel;

namespace TheatricalPlayersRefactoringKata.Infrastructure.RabbitMq
{
    /// <summary>
    /// Manages the connection and channel with the RabbitMQ server.
    /// Configures exchanges, queues, and QoS settings.
    /// </summary>
    public class RabbitMqClient
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqClient"/> class.
        /// </summary>
        /// <param name="rbMqSettings">The RabbitMQ settings.</param>
        public RabbitMqClient(RabbitMqSettings rbMqSettings)
        {
            var factory = new ConnectionFactory()
            {
                HostName = rbMqSettings.HostName,
                UserName = rbMqSettings.UserName,
                Password = rbMqSettings.Password,
                VirtualHost = rbMqSettings.VirtualHost,
                Port = rbMqSettings.Port,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        /// <summary>
        /// Configures the RabbitMQ channel by declaring exchanges, queues, and QoS settings.
        /// </summary>
        public void ConfigureStatementChannel()
        {
            // Declare an exchange named "statement_exchange" of type Direct
            _channel.ExchangeDeclare(exchange: "statement_exchange", 
                type: ExchangeType.Direct);

            // Define arguments for the queue, including Dead Letter Exchange (DLX) settings
            var arguments = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", "dlx_exchange" },
                { "x-dead-letter-routing-key", "dlx_key" }
            };

            // Declare a queue named "statement" with the specified properties
            _channel.QueueDeclare(queue: 
                "statement", durable: true, 
                exclusive: false, 
                autoDelete: false, 
                arguments: arguments);

            // Bind the "statement" queue to the "statement_exchange" using the routing key "statement_key"
            _channel.QueueBind(queue: "statement", 
                exchange: "statement_exchange", 
                routingKey: "statement_key");

            // Configure Quality of Service (QoS) settings for the channel
            _channel.BasicQos(prefetchSize: 0, 
                prefetchCount: 1, 
                global: false);
        }

        /// <summary>
        /// Gets the configured RabbitMQ channel.
        /// </summary>
        /// <returns>The RabbitMQ channel.</returns>
        public IModel GetChannel() => _channel;

        /// <summary>
        /// Gets the RabbitMQ connection.
        /// </summary>
        /// <returns>The RabbitMQ connection.</returns>
        public IConnection GetConnection() => _connection;
    }
}
