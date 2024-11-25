using Aplication.DTO;
using Aplication.Services.Formatters;
using Aplication.Services.Interfaces;
using Aplication.Services.XML;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.AccessControl;
using System.Text.Json;

namespace Aplication.Services.Queue
{
    public class ServiceBusConsumer
    {
        private readonly string _queueName;
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _savePath;
        private readonly XmlFileService _xmlFileService;
        public ServiceBusConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _queueName = configuration["AzureServiceBus:QueueName"];
            _client = new ServiceBusClient(configuration["AzureServiceBus:ConnectionString"]);
            _processor = _client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
            _serviceProvider = serviceProvider;
            _savePath = configuration["SavePath"];
            _xmlFileService = new XmlFileService(_savePath);
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
            await DoWork(message);
            await args.CompleteMessageAsync(args.Message);
        }


        private async Task DoWork(string message)
        {
            var invoiceDto = JsonSerializer.Deserialize(message, typeof(InvoiceDto));
            using var scope = _serviceProvider.CreateScope();
            var statementService = scope.ServiceProvider.GetRequiredService<IStatementService>();
            var xml = statementService.Print((InvoiceDto)invoiceDto!, new XmlInvoiceFormatter());
            await _xmlFileService.SaveXmlAsync(xml);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            string marcacao = "".PadLeft(20, '#');
            Console.WriteLine($"{marcacao}Erro no processamento: {args.Exception.Message}\r\n{marcacao}");
            return Task.CompletedTask;
        }
    }
}
