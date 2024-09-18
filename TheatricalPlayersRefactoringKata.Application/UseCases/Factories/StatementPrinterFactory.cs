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
                    return new TextStatementPrinterService();
                case "xml":
                    return new XmlStatementPrinterService();
                // Adicione mais casos conforme necessário para novos formatos
                default:
                    throw new ArgumentException("Invalid format", nameof(format));
            }
        }
    }
}
