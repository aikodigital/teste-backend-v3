using System.Net;
using System.Text;
using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services.Formatters;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Repositories;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPerformanceRepository _performanceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, IPerformanceRepository performanceRepository)
        {
            _invoiceRepository = invoiceRepository;
            _performanceRepository = performanceRepository;
        }

        public async Task<ServiceResponse<InvoiceResponse>> CreateInvoice(InvoiceRequest invoiceRequest)
        {
            var response = new ServiceResponse<InvoiceResponse>();

            var perfs = await _performanceRepository.GetPerformancesByIds(invoiceRequest.PerformanceIds);

            if (!perfs.Any())
            {
                response.Data = null;
                response.Message = "No valid performances found.";
                response.Status = HttpStatusCode.BadRequest;
                return response;
            }

            var invoice = new Invoice
            {
                Customer = invoiceRequest.Customer,
                Performances = perfs.ToList(),
            };

            await _invoiceRepository.CreateInvoice(invoice);

            var invoiceResponse = new InvoiceResponse(
                    invoice.Id,
                    invoice.Customer,
                    perfs.Select(p => new PerformanceResponse(
                        p.Id,
                        p.PlayId,
                        p.Audience,
                        p.Credits)).ToList());


            response.Data = invoiceResponse;
            response.Status = HttpStatusCode.OK;

            return response;
        }

        public async Task<(byte[], string, string)> GenerateStatement(Guid invoiceId, Formats format)
        {
            var invoice = await _invoiceRepository.GetInvoiceById(invoiceId);
            string mimeType;
            string fileExtension;

            IStatementFormatter statementFormatter;

            switch (format)
            {
                case Formats.TXT:
                    statementFormatter = new TextStatementFormatter();
                    mimeType = "text/plain";
                    fileExtension = "txt";
                    break;
                case Formats.XML:
                    statementFormatter = new XmlStatementFormatter();
                    mimeType = "application/xml";
                    fileExtension = "xml";
                    break;
                default:
                    throw new ArgumentException("Invalid Format");

            }

            var statementPrinter = new StatementPrinter(statementFormatter);
            var formattedContent = statementPrinter.Print(invoice);

            return (Encoding.UTF8.GetBytes(formattedContent), mimeType, fileExtension);
        }

        public async Task<ServiceResponse<IEnumerable<InvoiceResponse>>> GetInvoices()
        {
            var response = new ServiceResponse<IEnumerable<InvoiceResponse>>();

            var invoices = await _invoiceRepository.GetInvoices();

            var invoiceResponse = invoices.Select(i => new InvoiceResponse(
                    i.Id,
                    i.Customer,
                    i.Performances.Select(p => new PerformanceResponse(
                        p.Id,
                        p.PlayId,
                        p.Audience,
                        p.Credits)).ToList()));

            response.Data = invoiceResponse;
            response.Status = HttpStatusCode.OK;

            return response;

        }
    }
}