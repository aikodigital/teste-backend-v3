using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace Aplication.Services.Queue
{
    public class ServiceBusConsumer
    {
        private readonly string _queueName;
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;

        public ServiceBusConsumer(IConfiguration configuration)
        {
            _queueName = configuration["AzureServiceBus:QueueName"];
            _client = new ServiceBusClient(configuration["AzureServiceBus:ConnectionString"]);
            _processor = _client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
        }

        public async Task StartProcessingAsync()
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync();
        }

        public async Task StopProcessingAsync()
        {
            Console.WriteLine("Parando o processamento de mensagens.");
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
            await _client.DisposeAsync();
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();
            Console.WriteLine($"Mensagem recebida: {message}");
            DoWork();
            await args.CompleteMessageAsync(args.Message);
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
