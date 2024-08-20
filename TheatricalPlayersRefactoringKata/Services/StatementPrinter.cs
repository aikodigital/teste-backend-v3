using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Modules;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class StatementPrinter
    {
        public string? Print(Invoice invoice, Dictionary<string, Play> plays, OutputWritter outputWritter, string? filePath)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");

            decimal totalOwedAmount = 0;
            decimal totalEarnedCredits = 0;

            foreach (Performance performance in invoice.Performances)
            {
                if (!plays.ContainsKey(performance.PlayId))
                {
                    throw new KeyNotFoundException($"Unknown play {performance.PlayId}");
                }

                Play play = plays[performance.PlayId];

                decimal owedAmount = play.Type.CalculateAmount(performance, play);
                decimal earnedCredits = play.Type.CalculateCredit(performance, play);

                performance.Results = new PerformanceResults
                {
                    AmountOwed = owedAmount,
                    EarnedCredits = earnedCredits,
                    Play = play
                };

                totalOwedAmount += owedAmount;
                totalEarnedCredits += earnedCredits;
            }

            invoice.Results = new InvoiceResults
            {
                TotalAmountOwed = totalOwedAmount,
                TotalEarnedCredits = totalEarnedCredits
            };

            // Generate the output and revert it back to a string
            byte[] output = outputWritter.GenerateOutput(invoice, cultureInfo);

            if (filePath != null)
            {
                try
                {
                    File.WriteAllBytes(filePath, output);
                }
                catch (Exception exception)
                {
                    Console.Error.WriteLine($"Failed to write output to {filePath} with error: {exception.Message}");
                }
            }

            return Encoding.UTF8.GetString(output);
        }
    }
}
