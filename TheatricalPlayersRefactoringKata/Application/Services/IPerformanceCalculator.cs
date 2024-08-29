using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public interface IPerformanceCalculator
{
    Task <int> CalculateAmount(Performance performance, Play play);
    Task <int> CalculateVolumeCredits(Performance performance, Play play);

}
