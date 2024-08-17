using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Application.Request;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public class StatementProcessingService
    {
        private readonly BlockingCollection<InvoiceRequest> _invoiceQueue = new BlockingCollection<InvoiceRequest>();
        private readonly IStatementGenerator _statementGenerator;
        private readonly string _outputDirectory;
        private readonly ILogger<StatementProcessingService> _logger;

        public StatementProcessingService(
            IStatementGenerator statementGenerator,
            string outputDirectory,
            ILogger<StatementProcessingService> logger)
        {
            _statementGenerator = statementGenerator ?? throw new ArgumentNullException(nameof(statementGenerator));
            _outputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Directory.CreateDirectory(_outputDirectory);

            Task.Run(() => ProcessQueueAsync());
        }

        public void QueueInvoice(InvoiceRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "O parâmetro 'request' não pode ser nulo.");

            _invoiceQueue.Add(request);
        }

        private async Task ProcessQueueAsync()
        {
            foreach (var request in _invoiceQueue.GetConsumingEnumerable())
            {
                try
                {
                    var playsList = request.Plays.ToList();

                    var xmlContent = _statementGenerator.Generate(request.Invoice, playsList);
                    var filePath = Path.Combine(_outputDirectory, $"{request.Invoice.Customer}.xml");

                    await File.WriteAllTextAsync(filePath, xmlContent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar o extrato para o cliente '{Customer}' com ID '{InvoiceId}'.",
                        request.Invoice.Customer, request.Invoice.Id);

                    throw new InvalidOperationException("Ocorreu um erro ao processar o extrato.", ex);
                }
            }
        }
    }
}