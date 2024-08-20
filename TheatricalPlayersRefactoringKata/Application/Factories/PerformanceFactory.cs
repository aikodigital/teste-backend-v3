using Microsoft.Extensions.DependencyInjection;
using System;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;

namespace TheatricalPlayersRefactoringKata.Application.Factories
{
    public class PerformanceFactory : IPerformanceCalculatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PerformanceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPerformanceCalculator CreateCalculator(string genre)
        {
            return genre.ToLower() switch
            {
                "tragedy" => _serviceProvider.GetRequiredService<TragedyCalculator>(),
                "comedy" => _serviceProvider.GetRequiredService<ComedyCalculator>(),
                "historical" => _serviceProvider.GetRequiredService<HistoricalCalculator>(),
                _ => throw new ArgumentException($"Gênero '{genre}' não suportado")
            };
        }
    }
}