using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.UseCases.Factories;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UnitTests
{
    public class TragedyCalculatorTests
    {
        [Fact]
        public void TestCalculateAmount()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("hamlet", 55),
                }
            );

            Performance perf = invoice.Performances[0];
            var play = plays[perf.PlayId];
            var calculator = TheatricalCalculatorFactory.GetCalculator(play.Type);

            var thisAmount = calculator.CalculateAmount(perf, play);
            var volumeCredits = calculator.CalculateVolumeCredits(perf);

            Assert.Equal(65000m, thisAmount);
        }

        [Fact]
        public void TestCalculateVolumeCredits()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("hamlet", 55),
                }
            );

            Performance perf = invoice.Performances[0];
            var play = plays[perf.PlayId];
            var calculator = TheatricalCalculatorFactory.GetCalculator(play.Type);

            var volumeCredits = calculator.CalculateVolumeCredits(perf);

            Assert.Equal(25, volumeCredits);
        }
    }
}
