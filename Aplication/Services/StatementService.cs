using Aplication.DTO;
using Aplication.Interfaces;
using Aplication.Services.Calculators;
using Aplication.Services.Formatters;
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
    public class StatementService
    {
        private List<PlayDto> Plays = new()
            {
                new PlayDto("Hamlet", 4024, PlayType.tragedy),
                new PlayDto("As You Like It", 2670, PlayType.comedy),
                new PlayDto("Othello", 3560,        PlayType.tragedy),
                new PlayDto("Henry V", 3227, PlayType.history),
                new PlayDto("King John", 2648,   PlayType.history),
                new PlayDto("Richard III", 3718, PlayType.history)
            };
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
        => Plays.FirstOrDefault(x => x.Name.Contains(name))!;

        public InvoiceDto ObterInvoiceBigCo()
        => new("BigCo", GetPerformancesByName("Hamlet", "As You Like",
                "Othello"));


        public InvoiceDto ObterInvoiceBigCo2()
        => new("BigCo", GetPerformancesByName("Hamlet", "As You Like", "Othello", "Henry", "John", "Henry"));

        public static string Print(InvoiceDto invoice, IInvoiceFormatter formatter)
        {
            var (performances, valorTotal, valorCreditos) = InvoiceProcessor.Processar(invoice);
            return formatter.Format(invoice, valorTotal, valorCreditos, performances); 
        }
    }
}
