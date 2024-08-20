using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [ApiController]
    [Route("billing")]
    public class BillController : ControllerBase
    {
        [HttpPost("txt")]
        public async Task<IActionResult> GetTextBillingStatement([FromBody] Invoice invoice)
        {
            IPlayCalculator billTypeCalculator;

            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };

            decimal totalAmount = 0m;
            decimal volumeCredits = 0;

            CultureInfo cultureInfo = new CultureInfo("en-US");
            var billingStatement = new StringBuilder($"Statement for {invoice.Customer}\n");

            foreach (Performance perf in invoice.Performances)
            {
                Play play = plays[perf.PlayId];
                billTypeCalculator = BillProvider.GetCalculatorByType(play.Type);
                decimal thisAmount = billTypeCalculator.CalculateAmount(play, perf.Audience);
                volumeCredits += billTypeCalculator.CalculateCredits(play, perf.Audience);

                billingStatement.AppendLine(cultureInfo, $"  {play.Name}: {thisAmount:C} ({perf.Audience} seats)");
                totalAmount += thisAmount;
            }
            billingStatement.AppendLine(cultureInfo, $"Amount owed is {totalAmount:C}");
            billingStatement.AppendLine($"You earned {volumeCredits} credits");

            string path = Path.Combine(Environment.CurrentDirectory, "orders", $"{DateTime.Now:ddMMyyHHmmss}_{invoice.Customer}_order.txt");
            await System.IO.File.WriteAllTextAsync(path, billingStatement.ToString());

            return Ok(billingStatement.ToString());
        }
    }
}
