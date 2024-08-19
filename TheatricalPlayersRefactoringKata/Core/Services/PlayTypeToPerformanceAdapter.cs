using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Services
{
    public class PlayTypeToPerformanceAdapter : IPerformanceCalculator
    {
        private readonly IPlayTypeCalculator _playTypeCalculator;

        public PlayTypeToPerformanceAdapter(IPlayTypeCalculator playTypeCalculator)
        {
            _playTypeCalculator = playTypeCalculator;
        }

        public decimal CalculatePrice(Performance performance)
        {
            if (_playTypeCalculator.CanHandle(performance.Play.Genre.ToString()))
            {
                return _playTypeCalculator.CalculateAmount(performance.Play, performance);
            }

            throw new ArgumentException($"Tipo de peça não suportado: {performance.Play.Genre}");
        }

        public int CalculateCredits(Performance performance)
        {
            if (_playTypeCalculator.CanHandle(performance.Play.Genre.ToString()))
            {
                return _playTypeCalculator.CalculateCredits(performance.Play, performance);
            }

            throw new ArgumentException($"Tipo de peça não suportado: {performance.Play.Genre}");
        }
    }
}