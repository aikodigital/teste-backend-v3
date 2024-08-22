using System.IO;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Repositories;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<string> GenerateInvoiceXmlAsync(InvoiceRequest invoiceRequest)
        {
            var invoice = await _invoiceRepository.GetInvoiceAsync(invoiceRequest.InvoiceId);
            var plays = await _invoiceRepository.GetPlaysAsync();
            var filePath = Path.Combine(Path.GetTempPath(), $"invoice_{invoiceRequest.InvoiceId}.xml");

            var xmlPrinter = new XmlStatementPrinter();
            var xmlContent = xmlPrinter.Print(invoice, plays);
            await File.WriteAllTextAsync(filePath, xmlContent);

            return filePath;
        }
    }
}
