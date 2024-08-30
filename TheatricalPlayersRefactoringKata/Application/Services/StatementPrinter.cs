using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public class StatementPrinter
{
    private readonly IFormatterAdapter _formatterAdapter;

    public StatementPrinter(IFormatterAdapter formatterAdapter)
    {
        _formatterAdapter = formatterAdapter;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;

        return _formatterAdapter.Format(invoice, plays, totalAmount, volumeCredits);
    }
}