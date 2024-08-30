using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface IStatementPrinterService
{
    Statement BuildStatement(Invoice invoice, Dictionary<string, Play> plays);
    string Print(Invoice invoice, Dictionary<string, Play> plays);
    string Print(Statement statement);
}
