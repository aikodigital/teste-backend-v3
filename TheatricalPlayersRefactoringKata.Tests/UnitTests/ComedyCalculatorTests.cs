using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.UseCases.Factories;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UnitTests
{
    public class ComedyCalculatorTests
    {
        [Fact]
        public void TestCalculateAmount()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("as-like", 35),
                }
            );

            Performance perf = invoice.Performances[0];
            var play = plays[perf.PlayId];
            var calculator = TheatricalCalculatorFactory.GetCalculator(play.Type);

            var thisAmount = calculator.CalculateAmount(perf, play);
            var volumeCredits = calculator.CalculateVolumeCredits(perf);

            Assert.Equal(54700m, thisAmount);
        }

        [Fact]
        public void TestCalculateVolumeCredits()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("as-like", 35),
                }
            );

            Performance perf = invoice.Performances[0];
            var play = plays[perf.PlayId];
            var calculator = TheatricalCalculatorFactory.GetCalculator(play.Type);

            var volumeCredits = calculator.CalculateVolumeCredits(perf);

            Assert.Equal(12, volumeCredits);
        }
    }
}
