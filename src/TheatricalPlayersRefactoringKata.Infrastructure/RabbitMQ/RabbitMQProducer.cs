using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TheatricalPlayersRefactoringKata.Interface;

namespace TheatricalPlayersRefactoringKata.Infrastructure.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("printerQueue", exclusive: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "printerQueue", body: body);
        }
    }
}
