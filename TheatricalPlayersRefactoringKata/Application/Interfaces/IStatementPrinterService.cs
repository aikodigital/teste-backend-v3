using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface IStatementPrinterService
{
    Statement BuildStatement(Invoice invoice, Dictionary<string, Play> plays);
    string Print(Invoice invoice, Dictionary<string, Play> plays);
    string Print(Statement statement);
}
