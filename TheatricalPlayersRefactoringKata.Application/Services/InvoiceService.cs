using System.Net;
using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;
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
    }
}