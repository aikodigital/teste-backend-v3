using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public class XmlStatementGenerator : IStatementGenerator
    {
        private readonly IEnumerable<IPlayTypeCalculator> _calculators;
        private readonly ILogger<XmlStatementGenerator> _logger;

        public XmlStatementGenerator(IEnumerable<IPlayTypeCalculator> calculators, ILogger<XmlStatementGenerator> logger)
        {
            _calculators = calculators ?? throw new ArgumentNullException(nameof(calculators), "O parâmetro calculators não pode ser nulo.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "O parâmetro logger não pode ser nulo.");
        }

        public string Generate(Invoice invoice, Dictionary<Guid, Play> plays)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice), "O parâmetro invoice não pode ser nulo.");

            if (plays == null)
                throw new ArgumentNullException(nameof(plays), "O parâmetro plays não pode ser nulo.");

            var root = new XElement("Statement",
                new XElement("Customer", invoice.Customer),
                new XElement("Items")
            );

            decimal totalAmount = 0;
            int totalCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                if (!plays.TryGetValue(perf.PlayId, out var play))
                {
                    _logger.LogWarning($"ID da peça não encontrado: {perf.PlayId}");
                    throw new KeyNotFoundException($"A peça com o ID '{perf.PlayId}' não foi encontrada.");
                }

                var calculator = _calculators.FirstOrDefault(c => c.CanHandle(play.Genre.ToString()));

                if (calculator == null)
                    throw new ArgumentException($"Nenhum calculador disponível para o tipo de peça: {play.Genre}");

                try
                {
                    var amount = calculator.CalculateAmount(play, perf);
                    var credits = calculator.CalculateCredits(play, perf);

                    totalAmount += amount;
                    totalCredits += credits;

                    root.Element("Items").Add(new XElement("Item",
                        new XElement("AmountOwed", amount),
                        new XElement("EarnedCredits", credits),
                        new XElement("Seats", perf.Audience)
                    ));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao gerar o extrato.");
                    throw new InvalidOperationException("Erro ao gerar o extrato.", ex);
                }
            }

            root.Add(new XElement("AmountOwed", totalAmount));
            root.Add(new XElement("EarnedCredits", totalCredits));

            return root.ToString();
        }
    }
}