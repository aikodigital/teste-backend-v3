using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TheatricalPlayersRefactoringKata.Application.Printer;
using TheatricalPlayersRefactoringKata.Application.Request;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.PrinterWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "printerQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray(); ;
                    var message = Encoding.UTF8.GetString(body);
                    var objeto = System.Text.Json.JsonSerializer.Deserialize<PrinterWorkerRequest>(message);

                    var performances = objeto.Invoice.Performances.Select(p => new Performance(p.PlayId, p.Audience)).ToList();

                    var plays = new Dictionary<string, Play>
                        {
                            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                            { "as-like", new Play("As You Like It", 2670, "comedy") },
                            { "othello", new Play("Othello", 3560, "tragedy") }
                        };

                    var teste = new StatementPrinter().Print(new Invoice(objeto.Invoice.Customer, performances), plays, objeto.Type);
                    File.WriteAllText($"E:\\Dev\\Aiko\\teste-backend-v3\\invoices\\{objeto.Invoice.Customer}-{DateTime.Now:yyyyMMdd_HHmmss}.{objeto.Type}", teste);
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };
            channel.BasicConsume(queue: "printerQueue",
                                 autoAck: false,
                                 consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
