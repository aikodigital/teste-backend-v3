using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Models;
using Microsoft.Extensions.Configuration;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public class StatementProcessingService : BackgroundService, IStatementProcessingService
{
    private readonly ConcurrentQueue<Invoice> _invoiceQueue = new ConcurrentQueue<Invoice>();
    private readonly IStatementPrinterService _statementPrinterService;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _outputDirectory;

    public StatementProcessingService(
        IStatementPrinterService statementPrinterService,
        IConfiguration outputDirectory,
        IServiceProvider serviceProvider)
    {
        _statementPrinterService = statementPrinterService;
        _serviceProvider = serviceProvider;
        _outputDirectory = outputDirectory["BackgroundWorker:XmlDirectory"];
    }

    public void EnqueueInvoice(Invoice invoice)
    {
        _invoiceQueue.Enqueue(invoice);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            while (_invoiceQueue.TryDequeue(out var invoice))
            {
                await ProcessInvoiceAsync(invoice, stoppingToken);
            }

            await Task.Delay(100, stoppingToken);
        }
    }

    public async Task ProcessInvoiceAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        var plays = await GetPlaysAsync();

        var statement = await Task.Run(() => _statementPrinterService.BuildStatement(invoice, plays));

        var xmlContent = await Task.Run(() => _statementPrinterService.Print(statement));

        await Task.Run(() => {
            using (var scope = _serviceProvider.CreateScope())
            {
                var statementService = scope.ServiceProvider.GetRequiredService<IStatementService>();
                statementService.AddStatementAsync(statement);
            }
        });

        var fileName = $"{invoice.Customer}_{DateTime.Now:yyyyMMddHHmmss}.xml";

        var filePath = Path.Combine(_outputDirectory, fileName);

        Directory.CreateDirectory(_outputDirectory);

        await File.WriteAllTextAsync(filePath, xmlContent, cancellationToken);
    }

    private Task<Dictionary<string, Play>> GetPlaysAsync()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

        return Task.FromResult(plays);
    }
}
