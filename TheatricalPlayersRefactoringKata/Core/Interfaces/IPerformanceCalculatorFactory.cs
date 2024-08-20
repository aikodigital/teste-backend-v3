using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces
{
    public interface IPerformanceCalculatorFactory
    {
        IPerformanceCalculator CreateCalculator(string genre);
    }
}