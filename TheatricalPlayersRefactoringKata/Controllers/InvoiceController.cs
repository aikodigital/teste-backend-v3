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

        /// <summary>
        /// Endpoint for creating a new Invoice.
        /// </summary>
        /// <param name="customer">Customer name</param>
        /// <param name="performances">List of performances</param>
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

        /// <summary>
        /// Endpoint to return all Invoices from a specific Customer
        /// </summary>
        /// <param name="customerName">Customer name</param>
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

        /// <summary>
        /// Endpoint to return all Invoices from a specific Play
        /// </summary>
        /// <param name="playId">Play ID</param>
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

        /// <summary>
        /// Endpoint to delete a specific Invoice by Invoice Id
        /// </summary>
        /// <param name="id">Invoice ID</param>
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
