using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;

public interface IStatementGenerator
{
    string Generate(Invoice invoice, List<Play> plays);
}