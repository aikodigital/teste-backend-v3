using System.Text;
using System.Text.Json;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TS.Application.Services;
using TS.Domain.Enums;

namespace TS.Application.Workers
{
    public class RabbitMQWorker : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string queue_name = "invoices";
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQWorker(IServiceScopeFactory serviceScopeFactory)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _serviceScopeFactory = serviceScopeFactory;

            _channel.QueueDeclare(queue: queue_name,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body).Trim('"');
                var messageQueue = JsonSerializer.Deserialize<MessageQueue>(message);

                BackgroundJob.Enqueue(() => ProcessMessage(messageQueue!));

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: queue_name,
                                 autoAck: false,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        public void ProcessMessage(MessageQueue messageQueue)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var invoicesStatementsService = scope.ServiceProvider.GetRequiredService<IInvoicesStatementsServices>();

            invoicesStatementsService.GenerateFile((ETypeFile)messageQueue.TypeFile, messageQueue.InvoiceId).GetAwaiter().GetResult();
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}