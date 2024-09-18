using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Formatters;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Strategies;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly IStatementFormatterFactory _formatterFactory;
    private readonly Dictionary<string, IStatementStrategy> _strategies;

    public StatementPrinter()
    {
        _formatterFactory = new StatementFormatterFactory();
        _strategies = new Dictionary<string, IStatementStrategy> 
        {
            {"tragedy", new TragedyStrategy() },
            {"comedy", new ComedyStrategy() },
            {"history", new HistoricalStrategy() }
        };
    }
    public string Print(Invoice invoice, Dictionary<string, Play> plays, bool asXml = false)
    {
        var formatter = _formatterFactory.CreateFormatter(asXml);
        return formatter.Format(invoice, plays, _strategies);
    }
}
