using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Infrastructure.RabbitMq;
using TheatricalPlayersRefactoringKata.Infrastructure.Strategies.Exports;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public class StatementConsumer : BackgroundService
{
    private readonly IModel _channel;
    private readonly IEnumerable<IStatementExportStrategy> _invoiceExport;
    private readonly ILogger<StatementConsumer> _logger;

    public StatementConsumer(RabbitMqClient rabbitMqClient, 
        IEnumerable<IStatementExportStrategy> invoiceExport, 
        ILogger<StatementConsumer> logger)
    {
        _invoiceExport = invoiceExport;
        _logger = logger;
        _channel = rabbitMqClient.GetChannel();

        // Configure the RabbitMQ Statement channel
        rabbitMqClient.ConfigureStatementChannel();
    }

    /// <summary>
    /// Executes the background service to process invoices.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Deserialize the message to an invoice
            var invoice = JsonSerializer.Deserialize<Invoice>(message);

            if (invoice == null)
            {
                _logger.LogWarning("Failed to deserialize message: {Message}", message);
                return;
            }

            try
            {
                // Process the invoice
                await ProcessStatementAsync(invoice);

                // Acknowledge the message after successful processing
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                _logger.LogInformation("Message processed and acknowledged: {Message}", message);
            }
            catch (Exception ex)
            {
                //// Reject the message in case of failure
                _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                _logger.LogError(ex, "Failed to process message: {Message}", message);
            }
        };

        _channel.BasicConsume(queue: "statement",
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Processes the given invoice.
    /// </summary>
    /// <param name="invoice">The invoice to process.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task ProcessStatementAsync(Invoice invoice)
    {
        // Create a directory for the invoice
        var directoryPath = Path
            .Combine(Environment.CurrentDirectory, $"Statements/{invoice.Id}");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Export the invoice to the specified directory
        foreach (var invoiceExport in _invoiceExport)
        {
            await invoiceExport.ExportAsync(invoice, directoryPath);
        }
    }
}