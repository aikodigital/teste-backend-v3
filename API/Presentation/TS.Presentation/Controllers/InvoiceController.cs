using Microsoft.AspNetCore.Mvc;
using TS.Application.Invoices.Commands.AddInvoices.Request;
using TS.Application.Invoices.Commands.DeleteInvoices.Request;
using TS.Application.Invoices.Queries.GetAllInvoices.Request;
using TS.Application.Invoices.Queries.GetAllInvoices.Response;
using TS.Application.Invoices.Queries.GetInvoices.Request;
using TS.Presentation.ViewModels;

namespace TS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : BaseController
    {
        /// <summary>
        /// Get all Invoices
        /// </summary>
        /// <param name="term">Enter registration number</param>
        /// <returns>If there is a Invoice, return the Invoice, if not, return empty list</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllInvoicesResponse>))]
        public async Task<ActionResult> GetAll(string? term)
        {
            try
            {
                var request = new GetAllInvoicesRequest { Term = term };
                var result = await Mediator!.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve Invoices by id
        /// </summary>
        /// <param name="id">Invoice identifier</param> 
        /// <returns>If there is a Invoice, return the Invoice, if not, return null</returns>
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllInvoicesResponse>))]
        public async Task<ActionResult> Get(long id)
        {
            try
            {
                var request = new GetInvoicesRequest { Id = id };
                var result = await Mediator!.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registration creation
        /// </summary>
        /// <param name="viewModel">Object representing an Invoice</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Create([FromBody] AddInvoiceViewModel viewModel)
        {
            try
            {
                var request = new AddInvoicesRequest
                {
                    TypeFile = viewModel.TypeFile,
                    CustomerId = viewModel.CustomerId,
                    Seats = viewModel.Seats,
                    Performances = viewModel.Performances.Select(res => new AddInvoicePerformances
                    {
                        PlayId = res.PlayId,
                        Audience = res.Audience
                    })
                };

                await Mediator!.Send(request);

                return Ok("Fatura criada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Invoice
        /// </summary>
        /// <param name="id">Invoice identifier</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var request = new DeleteInvoicesRequest
                {
                    Id = id
                };

                await Mediator!.Send(request);

                return Ok("Fatura excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
