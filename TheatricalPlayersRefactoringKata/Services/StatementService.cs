using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementService
{
    private readonly IStatementFormatter _printer;

    public StatementService(IStatementFormatter printer)
    {
        _printer = printer;
    }

    public async Task<string> GenerateStatementAsync(Invoice invoice)
    {
        return await _printer.PrintAsync(invoice);
    }
}
