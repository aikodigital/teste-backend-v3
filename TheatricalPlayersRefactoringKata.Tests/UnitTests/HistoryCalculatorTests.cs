using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.UseCases.Factories;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UnitTests
{
    public class HistoryCalculatorTests
    {
        [Fact]
        public void TestCalculateAmount()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("henry-v", new Play("Henry V", 3227, "history"));

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("henry-v", 20),
                }
            );

            Performance perf = invoice.Performances[0];
            var play = plays[perf.PlayId];
            var calculator = TheatricalCalculatorFactory.GetCalculator(play.Type);

            var thisAmount = calculator.CalculateAmount(perf, play);

            Assert.Equal(70540m, thisAmount);
        }

        [Fact]
        public void TestCalculateVolumeCredits()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("henry-v", new Play("Henry V", 3227, "history"));

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("henry-v", 20),
                }
            );

            Performance perf = invoice.Performances[0];
            var play = plays[perf.PlayId];
            var calculator = TheatricalCalculatorFactory.GetCalculator(play.Type);

            var volumeCredits = calculator.CalculateVolumeCredits(perf);

            Assert.Equal(0, volumeCredits);
        }
    }
}
