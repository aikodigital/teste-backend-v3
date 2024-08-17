using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.UseCases;
using TheatricalPlayersRefactoringKata.Data.Dto;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class XmlStatementGenerator : StatementGenerator, IStatementGenerator
    {
        public string Generate(Invoice invoice, List<Play> plays)
        {
            var xmlInvoice = MapToXmlInvoice(invoice);

            var xmlSerializer = new XmlSerializer(typeof(XmlInvoice));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, xmlInvoice);
                return stringWriter.ToString();
            }
        }

        private XmlInvoice MapToXmlInvoice(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            return new XmlInvoice
            {
                Customer = invoice.Customer,
                TotalAmount = invoice.TotalAmount,
                TotalCredits = invoice.TotalCredits,
                Performances = invoice.Performances.Select(p => new XmlPerformance
                {
                    PlayId = p.PlayId,
                    Audience = p.Audience,
                    Genre = p.Genre.ToString()
                }).ToList()
            };
        }

        protected override string GenerateHeader(Invoice invoice)
        {
            return $"<invoice customer=\"{invoice.Customer}\">\n";
        }

        protected override string GeneratePerformanceDetail(Performance performance)
        {
            var genreString = performance.Genre.ToString();
            var calculator = PerformanceFactory.CreateCalculator(genreString);
            decimal price = calculator.CalculatePrice(performance);
            return $"\t<performance play=\"{performance.PlayId}\" seats=\"{performance.Audience}\" price=\"{price}\" />\n";
        }

        protected override string GenerateFooter(Invoice invoice)
        {
            return $"<total amount=\"{invoice.TotalAmount}\" credits=\"{invoice.TotalCredits}\" />\n</invoice>\n";
        }
    }
}