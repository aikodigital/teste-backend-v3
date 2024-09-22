using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Entities;

public interface IFormatter
{
    string Format(string customer, List<PerformanceResult> performances, int totalAmount, int credits);
}
