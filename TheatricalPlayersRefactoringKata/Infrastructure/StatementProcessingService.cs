using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Application.Request;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Data.Dto;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public class StatementProcessingService
    {
        private readonly BlockingCollection<InvoiceRequest> _invoiceQueue = new BlockingCollection<InvoiceRequest>();
        private readonly IStatementGenerator _statementGenerator;
        private readonly string _outputDirectory;
        private readonly ILogger<StatementProcessingService> _logger;
        private readonly Dictionary<string, IPerformanceCalculator> _calculators;  

        public StatementProcessingService(
            IStatementGenerator statementGenerator,
            string outputDirectory,
            ILogger<StatementProcessingService> logger,
            Dictionary<string, IPerformanceCalculator> calculators) 
        {
            _statementGenerator = statementGenerator ?? throw new ArgumentNullException(nameof(statementGenerator));
            _outputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _calculators = calculators ?? throw new ArgumentNullException(nameof(calculators));
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
                    var xmlInvoice = MapToXmlInvoice(request.Invoice, request.Plays);
                    var xmlContent = _statementGenerator.Generate(request.Invoice, request.Plays);
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

        private XmlInvoice MapToXmlInvoice(Invoice invoice, Dictionary<Guid, Play> playDictionary)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            return new XmlInvoice
            {
                Customer = invoice.Customer,
                TotalAmount = invoice.Performances.Sum(p => _calculators[playDictionary[p.PlayId].Genre.ToString()].CalculatePrice(p)),
                TotalCredits = invoice.Performances.Sum(p => _calculators[playDictionary[p.PlayId].Genre.ToString()].CalculateCredits(p)),
                Performances = invoice.Performances.Select(p => new XmlPerformance
                {
                    PlayId = p.PlayId,
                    Audience = p.Audience,
                    Amount = _calculators[playDictionary[p.PlayId].Genre.ToString()].CalculatePrice(p),
                    Credits = _calculators[playDictionary[p.PlayId].Genre.ToString()].CalculateCredits(p),
                    Genre = playDictionary[p.PlayId].Genre.ToString()
                }).ToList()
            };
        }
    }
}