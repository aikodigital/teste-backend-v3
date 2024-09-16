using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Interfaces;

public interface IStatementFormatter
{
    string Print(Invoice invoice);
}
