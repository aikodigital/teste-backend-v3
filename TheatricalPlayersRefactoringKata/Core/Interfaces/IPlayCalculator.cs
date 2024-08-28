using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IPlayCalculator
{
     Task <Money> CalculateCostAsync(Performance performance);
     Task <Credits> CalculateCreditsAsync(Performance performance);
}
