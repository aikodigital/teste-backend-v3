using Main.Contracts.StatementPrinter;

namespace Main.Application.Services.StatementPrinter
{
     public interface IStatementPrinterService
    {
        StatementPrinterResult Print(Invoice invoice, Dictionary<string, Play> plays);
    }
}
