using System;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Presentation;

namespace TheatricalPlayersRefactoringKata.Domain.Services.FormatterSelection;

public static class FormatterFactory
{
    public static IStatementFormatter GetFormatter(string format)
    {
        return format.ToLower() switch
        {
            "text" => new TextStatementPrinter(),
            "xml" => new XmlStatementPrinter(),
            _ => throw new Exception("Unknown format")
        };
    }
}
