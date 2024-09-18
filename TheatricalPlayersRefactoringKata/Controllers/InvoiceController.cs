using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("Create")]
        public Task<ActionResult> Create(string customer, List<PerformanceModel> performances)
        {
            try
            {
                return _invoiceService.Create(new InvoiceModel(customer, performances));
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("GetAllByCustomer")]
        public Task<ActionResult> GetAllByCustomer(string customerName)
        {
            try
            {
                return _invoiceService.GetAllByCustomer(customerName);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("GetAllByPlay")]
        public Task<ActionResult> GetAllByPlay(string playId)
        {
            try
            {
                return _invoiceService.GetAllByPlay(playId);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("Delete")]
        public Task<ActionResult> Delete(string id)
        {
            try
            {
                return _invoiceService.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
