using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Calculatros;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Formatters;

public class StatementPrinter
{
    private readonly XmlFormatter _xmlFormatter = new XmlFormatter();
    private readonly TextFormatter _textFormatter = new TextFormatter();

    public string Print(Invoice invoice, Dictionary<string, Play> plays, string format = "text")
    {
        if (format.Equals("xml", StringComparison.OrdinalIgnoreCase))
        {
            return _xmlFormatter.GenerateXml(invoice, plays);
        }
        else
        {
            return _textFormatter.GenerateText(invoice, plays);
        }
    }
}
