using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Formatters;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly IExtractFormatter _formatter;

    public StatementPrinter(IExtractFormatter formatter)
    {
        _formatter = formatter;
    }

    //Print results
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        
        return _formatter.Formatter(invoice, plays);
    }
}
