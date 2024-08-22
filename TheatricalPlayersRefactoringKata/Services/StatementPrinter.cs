using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    private readonly StatementCalculator _statementCalculator;

    public StatementPrinter(StatementCalculator statementCalculator)
    {
        _statementCalculator = statementCalculator;
    }

    public string Print(Invoice invoice, Dictionary<int, Play> plays)
    {
        return _statementCalculator.Calculate(invoice);
    }
}
