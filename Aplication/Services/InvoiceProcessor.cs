using Aplication.DTO;
using Aplication.Services.Calculators;
using CrossCutting;

namespace Aplication.Services
{
    public class InvoiceProcessor
    {
        public static (IEnumerable<PerformanceResult>, int valorTotal, int valorCreditos)
            Processar(InvoiceDto invoice)
        {
            var performances = new List<PerformanceResult>();
            int valorTotal = 0; int valorCreditos = 0;

            foreach (var perf in invoice.Performances)
            {
                int lines = perf.Play.Lines < 1000 ? 1000 : Math.Min(perf.Play.Lines, 4000);
                var valorPorPerformance = lines * 10;
                var earnedCredits = 0;

                var calculadora = PriceCalculatorFactory.GetCalculator(perf.PlayType);
                if (perf.PlayType == PlayType.history)
                    calculadora.ReservedValue = valorPorPerformance;

                valorPorPerformance += calculadora.CalculatePrice(perf.Audience);

                earnedCredits = Math.Max(perf.Audience - 30, 0);

                if (perf.PlayType == PlayType.comedy)
                    earnedCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                valorTotal += valorPorPerformance;
                valorCreditos += earnedCredits;

                performances.Add(new PerformanceResult
                {
                    PlayName = perf.Play.Name,
                    Audience = perf.Audience,
                    ValorPorPerformance = valorPorPerformance,
                    EarnedCredits = earnedCredits
                });
            }
            return (performances, valorTotal, valorCreditos);
        }
    }
}
