using TheatricalPlayersRefactoringKata.Application.Services.Printers;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Factories
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
