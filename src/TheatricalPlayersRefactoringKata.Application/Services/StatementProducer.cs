using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Infrastructure.RabbitMq;

namespace TheatricalPlayersRefactoringKata.Application.Services;
public class StatementProducer
{
    private readonly IModel _channel; 
    private readonly ILogger<StatementConsumer> _logger;

    public StatementProducer(RabbitMqClient rabbitMqClient, 
        ILogger<StatementConsumer> logger)
    {
        _logger = logger;
        _channel = rabbitMqClient.GetChannel();

        // Configure the RabbitMQ Statement channel
        rabbitMqClient.ConfigureStatementChannel();
    }

    /// <summary>
    /// Sends the given invoice to the RabbitMQ queue.
    /// </summary>
    /// <param name="invoice">The invoice to be sent to the queue.</param>
    public void SendToQueue(Invoice invoice)
    {
        var message = JsonSerializer.Serialize(invoice);
        var body = Encoding.UTF8.GetBytes(message);

        //// Create basic properties for the message
        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;

        // Publish the message to the queue
        // as a default exchange.
        _channel.BasicPublish(
            exchange: "statement_exchange",
            routingKey: "statement_key",
            basicProperties: properties,
            body: body);

        _logger.LogInformation("Sent message to queue: {Message}", message);
    }
}