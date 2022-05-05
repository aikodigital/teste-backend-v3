using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata;

public interface IStatementPrinter
{
    string Print(Statement statement, CultureInfo cultureInfo = null);
}
