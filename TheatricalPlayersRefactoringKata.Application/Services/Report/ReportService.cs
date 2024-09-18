using cliqx.pernambucanas.selecaorh.extensions;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Application.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IPlayService _playService;

        public ReportService(IInvoiceService invoiceService, IPlayService playService)
        {
            _invoiceService = invoiceService;
            _playService = playService;
        }

        public async Task<ActionResult> ReportByCustomer(List<string> customersNames)
        {
            ActionResult[]? results = null;

            while (customersNames.Count > 0)
            {
                var customerToProcess = customersNames.Pop(30);

                var tasksReport = customerToProcess.ConvertAll(customerName => GenerateReport(customerName));

                results = await Task.WhenAll(tasksReport);
            }

            return new OkObjectResult(results
                .Select(result => (OkObjectResult)result)
                .Select(okResult => okResult.Value));
        }

        private async Task<ActionResult> GenerateReport(string customerName)
        {
            var resultGetAllCustomer = await _invoiceService.GetAllByCustomer(customerName) as OkObjectResult;
            var resultGetAllPlays = await _playService.GetAll() as OkObjectResult;

            if (resultGetAllCustomer?.Value is not List<InvoiceModel> invoicesModel || invoicesModel.Count == 0)
                throw new Exception($"No customers with this name ({customerName})");

            if (resultGetAllPlays?.Value is not List<PlayModel> playsModel || playsModel.Count == 0)
                throw new Exception("No recorded plays");

            var unifyPerformances = new List<PerformanceModel>();
            invoicesModel.ForEach(invoice => unifyPerformances.AddRange(invoice.Performances));

            var unifyInvoice = new InvoiceModel(customerName, unifyPerformances);

            return new OkObjectResult(StatementPrinter.Print(unifyInvoice, playsModel, ReportType.XMLTOFILE));
        }
    }
}
