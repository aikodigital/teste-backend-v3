using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public interface IPerformanceValidationService
{
    void ValidatePerformances(List<Performance> performances);

}
