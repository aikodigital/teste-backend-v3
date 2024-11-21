using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Extensions
{
    public class TestExtension
    {
        //private void AssertPerformanceCredits(Performance performance, decimal expectedCredits)
        //{
        //    var strategy = GenreStrategyFactory.Create(performance.Play.Type);
        //    CalculatePerformanceCostAndCredits(performance, strategy);

        //    Assert.Equal(expectedCredits, performance.Credits);
        //}

        //private void AssertPerformanceBasePrice(Performance performance, decimal expectedCost)
        //{
        //    var strategy = GenreStrategyFactory.Create(performance.Play.Type);
        //    CalculatePerformanceCostAndCredits(performance, strategy);

        //    Assert.Equal(expectedCost, performance.BasePrice);
        //}
        //private void AssertPerformanceCost(Performance performance, decimal expectedCost)
        //{
        //    var strategy = GenreStrategyFactory.Create(performance.Play.Type);
        //    CalculatePerformanceCostAndCredits(performance, strategy);

        //    Assert.Equal(expectedCost, performance.Cost);
        //}
        //private void CalculatePerformanceCostAndCredits(Performance performance, IGenreStrategy strategyGenreFactory)
        //{


        //    performance.Cost = strategyGenreFactory.CalculateCost(performance.Audience, performance.Play.Lines);
        //    performance.Credits = strategyGenreFactory.CalculateCredits(performance.Audience);
        //    performance.BasePrice = strategyGenreFactory.CalculateBasePrice(performance.Play.Lines);
        //}
    }
}
