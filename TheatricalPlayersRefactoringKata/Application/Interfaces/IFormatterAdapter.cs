using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface IFormatterAdapter
{
    string Format(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, decimal volumeCredits);
}