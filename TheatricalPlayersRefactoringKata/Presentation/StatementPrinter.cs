using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Presentation;

public class StatementPrinter
{
    private readonly IStatementFormatter _statementFormatter;

    public StatementPrinter(IStatementFormatter statementFormatter)
    {
        _statementFormatter = statementFormatter;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        return _statementFormatter.Format(invoice, plays);
    }
}

