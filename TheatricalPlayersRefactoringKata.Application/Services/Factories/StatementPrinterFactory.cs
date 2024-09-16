using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services.Printers;

namespace TheatricalPlayersRefactoringKata.Application.Services.Factories
{
    public static class StatementPrinterFactory
    {
        public static IStatementPrinter GetPrinter(string format)
        {
            switch (format.ToLower())
            {
                case "text":
                    return new TextStatementPrinter();
                case "xml":
                    return new XmlStatementPrinter();
                // Adicione mais casos conforme necessário para novos formatos
                default:
                    throw new ArgumentException("Invalid format", nameof(format));
            }
        }
    }
}
