using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Infrastructure.Strategies.PrintStatement;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Factories;

public static class PrintStatementStrategyFactory
{
    public static IPrintStatementStrategy CreateStrategy(PrintFormatEnum printFormat)
    {
        return printFormat switch
        {
            PrintFormatEnum.Text => new TextStatementStrategy(),
            PrintFormatEnum.Xml => new XmlStatementStrategy(),
            _ => throw new Exception("unknown type: " + printFormat)
        };
    }
}