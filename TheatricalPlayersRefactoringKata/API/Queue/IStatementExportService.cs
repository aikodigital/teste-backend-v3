using TheatricalPlayersRefactoringKata.API.DTOs;

namespace TheatricalPlayersRefactoringKata.API.Queue
{
    public interface ITheaterStatementExportService
    {
        string GerarExtratoXml(TheaterInvoiceResponseDTO invoiceResponse);
    }
}
