using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata;

public class AsyncStatementProcessor
{
    private readonly StatementPrinter _statementPrinter;
    private readonly ConcurrentQueue<(Invoice invoice, Dictionary<string, Play> plays)> _queue;
    private readonly string _outputDirectory;

    public AsyncStatementProcessor(StatementPrinter statementPrinter, string outputDirectory)
    {
        _statementPrinter = statementPrinter;
        _queue = new ConcurrentQueue<(Invoice, Dictionary<string, Play>)>();
        _outputDirectory = outputDirectory;
    }

    public void EnqueueInvoice(Invoice invoice, Dictionary<string, Play> plays)
    {
        _queue.Enqueue((invoice, plays));
    }

    public async Task ProcessQueueAsync(CancellationToken cancellationToken = default)
    {
        while (_queue.TryDequeue(out var item))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            var xmlStatement = _statementPrinter.PrintXml(item.invoice, item.plays);
            var fileName = $"{item.invoice.Customer}_{DateTime.UtcNow:yyyyMMddHHmmss}.xml";
            var filePath = Path.Combine(_outputDirectory, fileName);

            await File.WriteAllTextAsync(filePath, xmlStatement, cancellationToken);
        }
    }
}
