using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata;

public class StatementService
{
    private readonly IStatementFormatter _printer;

    public StatementService(IStatementFormatter printer)
    {
        _printer = printer;
    }

    public string GenerateStatement(Invoice invoice)
    {
        return _printer.Print(invoice);
    }
}
