using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Channels;
using System.Threading;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Domain.Exception;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Infra.Interfaces.Repository;
using System.Collections.Concurrent;

namespace TheatricalPlayersRefactoringKata.Domain.Services
{
    public class StatementQueueService
    {
        private readonly Channel<Invoice> _invoiceChannel;
        private readonly StatementPrinter _statementPrinter;
        private readonly IStatementFormatter _formatter;
        private readonly string _outputDirectory;
        private readonly ILogger<IPerformanceRepository> _logger;
        private readonly ConcurrentQueue<Invoice> _invoiceQueue;
        private int _pendingInvoices; // Contador para rastrear as invoices pendentes
        private SemaphoreSlim _processingSemaphore = new SemaphoreSlim(1, 1); // Semáforo para garantir thread-safety

        public StatementQueueService(
            StatementPrinter statementPrinter,
            IStatementFormatter formatter,
            string outputDirectory, 
            ILogger<StatementQueueService> logger)
        {
            _invoiceChannel = Channel.CreateUnbounded<Invoice>();
            _statementPrinter = statementPrinter;
            _formatter = formatter;
            _outputDirectory = outputDirectory;
            _invoiceQueue = new ConcurrentQueue<Invoice>();

            if (!Directory.Exists(_outputDirectory))
            {
                Directory.CreateDirectory(_outputDirectory);
            }
        }

        public async Task EnqueueInvoiceAsync(Invoice invoice)
        {
            await _processingSemaphore.WaitAsync();
            try
            {
                _invoiceQueue.Enqueue(invoice);
                Interlocked.Increment(ref _pendingInvoices);
            }
            finally
            {
                _processingSemaphore.Release();
            }
        }

        public async Task StartProcessingAsync(Dictionary<string, Play> plays, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested || _pendingInvoices > 0) 
            {
                Invoice invoice = null;

                await _processingSemaphore.WaitAsync();
                try
                {
                    if (_invoiceQueue.TryDequeue(out invoice))
                    {
                        Interlocked.Decrement(ref _pendingInvoices);
                    }
                }
                finally
                {
                    _processingSemaphore.Release();
                }

                if (invoice != null)
                {
                    try
                    {
                        var statement = _statementPrinter.GenerateStatement(invoice, plays);
                        var result = _formatter.Print(statement);
                        var filePath = Path.Combine(_outputDirectory, $"{invoice.Customer}_{Guid.NewGuid()}.xml");

                        Directory.CreateDirectory(_outputDirectory);
                        await File.WriteAllTextAsync(filePath, result);
                    }
                    catch (BusinessException ex)
                    {
                        _logger.LogError(ex.Message);
                        throw new BusinessException(ex.Message);
                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }

        public int PendingCount => _pendingInvoices; 
    }
}
