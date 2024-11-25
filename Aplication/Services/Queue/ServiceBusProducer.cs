using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace Aplication.Services.Queue
{
    public class ServiceBusProducer
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public ServiceBusProducer(IConfiguration configuration)
        {
            _connectionString = configuration["AzureServiceBus:ConnectionString"];
            _queueName = configuration["AzureServiceBus:QueueName"];
        }

        public async Task SendMessageAsync(string message)
        {
            await using var client = new ServiceBusClient(_connectionString);
            var sender = client.CreateSender(_queueName);

            var serviceBusMessage = new ServiceBusMessage(message);
            await sender.SendMessageAsync(serviceBusMessage);
        }
    }
}
