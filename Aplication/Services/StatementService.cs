using Aplication.DTO;
using Aplication.Interfaces;
using Aplication.Services.Calculators;
using Aplication.Services.Formatters;
using Aplication.Services.Interfaces;
using CrossCutting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class StatementService : IStatementService
    {
        PlayService playService = new PlayService();
        private List<PerformanceDto> ListAllPerformances()
        {
            List<PerformanceDto> performances = new()
            {
                new(GetPlayByName("Hamlet"), 55),
                new(GetPlayByName("You Like"), 35),
                new(GetPlayByName("Othello"), 40),
                new(GetPlayByName("Henry"), 20),
                new(GetPlayByName("John"), 39)
                };
            return performances;
        }

        private List<PerformanceDto> GetPerformancesByName(params string[] names)
        {
            var AllPerformances = ListAllPerformances();
            var perfs = names.Select(name => AllPerformances.FirstOrDefault(perf => perf.Play.Name.Contains(name)))
                .ToList();
            return perfs!;
        }

        private PlayDto GetPlayByName(string name)
        => playService.GetPlays().FirstOrDefault(x => x.Name.Contains(name))!;

        public InvoiceDto ObterInvoiceBigCo()
        => new("BigCo", GetPerformancesByName("Hamlet", "As You Like",
                "Othello"));


        public InvoiceDto ObterInvoiceBigCo2()
        => new("BigCo", GetPerformancesByName("Hamlet", "As You Like", "Othello", "Henry", "John", "Henry"));

        public string Print(InvoiceDto invoice, IInvoiceFormatter formatter)
        {
            var (performances, valorTotal, valorCreditos) = InvoiceProcessor.Processar(invoice);
            return formatter.Format(invoice, valorTotal, valorCreditos, performances); 
        }
    }
}
