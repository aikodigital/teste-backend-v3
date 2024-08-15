using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public class XmlStatementGenerator : IStatementGenerator
    {
        private readonly IEnumerable<IPlayTypeCalculator> _calculators;

        public XmlStatementGenerator(IEnumerable<IPlayTypeCalculator> calculators)
        {
            _calculators = calculators ?? throw new ArgumentNullException(nameof(calculators), "O parâmetro 'calculators' não pode ser nulo.");
        }

        public string Generate(Invoice invoice, Dictionary<string, Play> plays)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice), "O parâmetro 'invoice' não pode ser nulo.");

            if (plays == null)
                throw new ArgumentNullException(nameof(plays), "O parâmetro 'plays' não pode ser nulo.");

            var root = new XElement("Extrato",
                new XElement("Cliente", invoice.Customer));

            foreach (var perf in invoice.Performances)
            {
                if (!plays.TryGetValue(perf.PlayId, out var play))
                    throw new KeyNotFoundException($"A peça com o ID '{perf.PlayId}' não foi encontrada.");

                var calculator = _calculators.FirstOrDefault(c => c.CanHandle(play.Type));

                if (calculator == null)
                    throw new ArgumentException($"Nenhum calculador disponível para o tipo de peça: {play.Type}");

                try
                {
                    var amount = calculator.CalculateAmount(play, perf);
                    var credits = calculator.CalculateCredits(play, perf);

                    root.Add(new XElement("Apresentacao",
                        new XElement("NomePeca", play.Name),
                        new XElement("Valor", amount),
                        new XElement("Publico", perf.Audience),
                        new XElement("Creditos", credits)
                    ));
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Erro ao gerar o extrato.", ex);
                }
            }

            return root.ToString();
        }
    }
}