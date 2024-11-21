using Aplication.DTO;
using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class StatementService
    {
        private static List<PerformanceDto> Performances = new()
        {
                new PerformanceDto(GetPlayByName("Hamlet"), 55),
                new PerformanceDto(GetPlayByName("You Like"), 35),
                new PerformanceDto(GetPlayByName("Othello"), 40),
                new PerformanceDto(GetPlayByName("Henry"), 20),
                new PerformanceDto(GetPlayByName("John"), 39),
                new PerformanceDto(GetPlayByName("Henry"), 20)
        };

        private static List<PlayDto> Plays = new()
            {
                new PlayDto("Hamlet", 4024, PlayType.tragedy),
                new PlayDto("As You Like It", 2670, PlayType.comedy),
                new PlayDto("Othello", 3560,        PlayType.tragedy),
                new PlayDto("Henry V", 3227, PlayType.history),
                new PlayDto("King John", 2648,   PlayType.history),
                new PlayDto("Richard III", 3718, PlayType.history)
            };

        public static string Print()//InvoiceDto invoice
        {
            InvoiceDto invoicex = new("BigCo", GetPerformances("Hamlet", "As You Like",
                "Othello"));

            return "";
        }

        private static List<PerformanceDto> GetPerformances(params string[] names)
        {
            var perfs = names.Select(name => Performances.FirstOrDefault(perf => perf.Play.Name.Contains(name)))
                .ToList();
            return perfs!;
        }


        private static PlayDto GetPlayByName(string name)
        => Plays.FirstOrDefault(x => x.Name.Contains(name))!;



    }
}
