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
        private List<PerformanceDto> ListAllPerformances()
        {
            List<PerformanceDto> performances = new()
            {
                new(GetPlayByName("Hamlet"), 55),
                new(GetPlayByName("You Like"), 35),
                new(GetPlayByName("Othello"), 40),
                new(GetPlayByName("Henry"), 20),
                new(GetPlayByName("John"), 39),
                new(GetPlayByName("Henry"), 20)
                };
            return performances;
        }

        private List<PlayDto> Plays = new()
            {
                new PlayDto("Hamlet", 4024, PlayType.tragedy),
                new PlayDto("As You Like It", 2670, PlayType.comedy),
                new PlayDto("Othello", 3560,        PlayType.tragedy),
                new PlayDto("Henry V", 3227, PlayType.history),
                new PlayDto("King John", 2648,   PlayType.history),
                new PlayDto("Richard III", 3718, PlayType.history)
            };

        public string Print()//InvoiceDto invoice
        {
            InvoiceDto invoicex = new("BigCo", GetPerformances("Hamlet", "As You Like",
                "Othello"));



            return "";
        }

        private List<PerformanceDto> GetPerformances(params string[] names)
        {
            var AllPerformances = ListAllPerformances();
            var perfs = names.Select(name => AllPerformances.FirstOrDefault(perf => perf.Play.Name.Contains(name)))
                .ToList();
            return perfs!;
        }


        private PlayDto GetPlayByName(string name)
        => Plays.FirstOrDefault(x => x.Name.Contains(name))!;



    }
}
