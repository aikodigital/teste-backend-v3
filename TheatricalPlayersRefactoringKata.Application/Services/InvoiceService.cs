using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Domain.Interface;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPlayRepository _playRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, IPlayRepository playRepository)
        {
            _invoiceRepository = invoiceRepository;
            _playRepository = playRepository;
        }

        public async Task<ActionResult> Create(InvoiceModel invoice)
        {
            try
            {
                var entity = InvoiceModel.ConvertToEntity(invoice);
                var existPlays = invoice.Performances.FindAll(perfom => _playRepository.GetByPlayId(perfom.PlayId).Result != null);

                if (existPlays.Count != invoice.Performances.Count)
                {
                    var exceptPerfom = invoice.Performances.Except(existPlays).ToList();
                    var selectPlaysId = exceptPerfom.Select(playsId => playsId.PlayId);

                    throw new Exception($"The following PlaysIds were not found: {string.Join(", ", selectPlaysId)}");
                }

                var result = await _invoiceRepository.Create(entity);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var result = await _invoiceRepository.Delete(id);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<ActionResult> GetAllByCustomer(string customerName)
        {
            try
            {
                var result = await _invoiceRepository.GetAllByCustomer(customerName);

                var model = InvoiceModel.ConvertToModels(result);

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<ActionResult> GetAllByPlay(string playId)
        {
            try
            {
                var result = await _invoiceRepository.GetAllByPlay(playId);

                var model = InvoiceModel.ConvertToModels(result);

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
