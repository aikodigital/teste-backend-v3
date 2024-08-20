using Microsoft.Extensions.DependencyInjection;
using System;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Services
{
    public class PerformanceCalculatorFactory : IPerformanceCalculatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PerformanceCalculatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPerformanceCalculator CreateCalculator(string genre)
        {
            switch (genre)
            {
                case "Tragedy":
                    return _serviceProvider.GetRequiredService<TragedyCalculator>();
                case "Comedy":
                    return _serviceProvider.GetRequiredService<ComedyCalculator>();
                case "Historical":
                    return new HistoricalCalculator(
                        _serviceProvider.GetRequiredService<TragedyCalculator>(),
                        _serviceProvider.GetRequiredService<ComedyCalculator>()
                    );
                default:
                    throw new ArgumentException($"Gênero inválido: {genre}", nameof(genre));
            }
        }
    }
}