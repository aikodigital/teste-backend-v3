using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{

    private readonly IStatementFormatter _formatter;

    public StatementPrinter(IStatementFormatter formatter)
    {
        _formatter = formatter;
    }

    public string Print(Invoice invoice)
    {
        var service = new StatementService(_formatter);
        return service.GenerateStatement(invoice);
    }

}
