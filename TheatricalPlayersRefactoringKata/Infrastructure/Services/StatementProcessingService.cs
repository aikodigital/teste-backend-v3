using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using TheatricalPlayersRefactoringKata.Application.UseCases;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Presentation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services;

public class StatementProcessingService : BackgroundService
{
    private readonly IStatementQueue _statementQueue;
    private readonly GenerateStatementUseCase _generateStatementUseCase;
    private readonly XmlStatementPrinter _xmlFormatter;
    private readonly ILogger<StatementProcessingService> _logger;

    public StatementProcessingService(
        IStatementQueue statementQueue,
        GenerateStatementUseCase generateStatementUseCase,
        XmlStatementPrinter xmlFormatter,
        ILogger<StatementProcessingService> logger)
    {
        _statementQueue = statementQueue;
        _generateStatementUseCase = generateStatementUseCase;
        _xmlFormatter = xmlFormatter;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Statement Processing Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_statementQueue.TryDequeueStatement(out var invoice))
                {
                    // Process the invoice
                    var statementResult = _generateStatementUseCase.GenerateExtractValues(invoice);
                    string xmlOutput = _xmlFormatter.Print(statementResult);

                    // Save XML to directory
                    SaveXmlToFile(invoice, xmlOutput);

                    _logger.LogInformation($"Processed invoice for {invoice.Customer}.");
                }
                else
                {
                    // No items in queue, wait for a short period
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing statements.");
            }
        }

        _logger.LogInformation("Statement Processing Service is stopping.");
    }

    private void SaveXmlToFile(Invoice invoice, string xmlContent)
    {
        // Create the directory in TheatricalPlayersRefactoringKata.Tests\bin\Debug\net6.0\ProcessedStatements
        string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProcessedStatements");
        Directory.CreateDirectory(directoryPath);

        string fileName = $"Statement_{invoice.Customer}_{DateTime.Now:yyyyMMddHHmmss}.xml";
        string filePath = Path.Combine(directoryPath, fileName);

        File.WriteAllText(filePath, xmlContent);
    }
}
