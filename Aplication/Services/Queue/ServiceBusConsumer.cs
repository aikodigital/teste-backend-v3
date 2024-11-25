using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace Aplication.Services.Queue
{
    public class ServiceBusConsumer
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public ServiceBusConsumer(IConfiguration configuration)
        {
            _connectionString = configuration["AzureServiceBus:ConnectionString"];
            _queueName = configuration["AzureServiceBus:QueueName"];
        }

        public async Task StartProcessingAsync()
        {
            await using var client = new ServiceBusClient(_connectionString);
            var processor = client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();
        }

        private Task MessageHandler(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();
            Console.WriteLine($"Mensagem recebida: {message}");
            DoWork();
            return Task.CompletedTask;
        }

        private void DoWork()
        {
            Console.WriteLine("Procesamento iniciado, Processamento concluído");
        }


        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            string marcacao = "".PadLeft(20, '#');
            Console.WriteLine($"{marcacao}Erro no processamento: {args.Exception.Message}\r\n{marcacao}");
            return Task.CompletedTask;
        }
    }


}
