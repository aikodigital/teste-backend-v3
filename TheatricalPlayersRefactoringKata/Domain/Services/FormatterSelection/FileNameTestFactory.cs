using System;

namespace TheatricalPlayersRefactoringKata.Domain.Services.FormatterSelection;

public class FileNameTestFactory
{
    public static string GetApprovedFileName(string format)
    {
        return format switch
        {
            "text" => "StatementPrinterTests.TestTextStatementExample",
            "xml" => "StatementPrinterTests.TestXmlStatementExample",
            _ => throw new ArgumentException($"Unknown format")
        };
    }
}
