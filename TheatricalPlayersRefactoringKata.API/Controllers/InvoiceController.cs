using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private static List<Invoice> invoices = new List<Invoice>();

        /// <summary>
        /// Recupera todas as faturas.
        /// </summary>
        /// <returns>Lista de faturas ou mensagem de erro</returns>
        /// <response code="200">Retorna todas as faturas</response>
        /// <response code="204">Nenhuma fatura encontrada</response>
        [HttpGet]
        public ActionResult<IEnumerable<Invoice>> GetAllInvoices()
        {
            if (!invoices.Any())
            {
                return NoContent();
            }
            return Ok(invoices);
        }

        /// <summary>
        /// Recupera uma fatura específica com base no nome informado.
        /// </summary>
        /// <returns>Fatura correspondente ou mensagem de erro</returns>
        /// <response code="200">Retorna a fatura especificada</response>
        /// <response code="404">Fatura não encontrada</response>
        [HttpGet("{customer}")]
        public ActionResult<Invoice> GetInvoiceByCustomer(string customer)
        {
            var invoice = invoices.Find(i => i.Customer == customer);
            if (invoice == null)
            {
                return NotFound("Fatura não encontrada.");
            }
            return Ok(invoice);
        }

        /// <summary>
        /// Cria uma nova fatura com base nos parametros fornecidos.
        /// </summary>
        /// <returns>Mensagem de sucesso ou erro</returns>
        /// <response code="201">Fatura criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Erro na criação da fatura</response>
        [HttpPost]
        public ActionResult<Invoice> CreateInvoice([FromBody] Invoice invoice)
        {
            if (invoice == null || string.IsNullOrWhiteSpace(invoice.Customer) || invoice.Performances == null || invoice.Performances.Count == 0)
            {
                return BadRequest("Dados da fatura são inválidos.");
            }

            try
            {
                invoices.Add(invoice);

                if (invoices.Any(i => i.Customer == invoice.Customer))
                {
                    return CreatedAtAction(nameof(GetInvoiceByCustomer), new { customer = invoice.Customer }, invoice);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar a fatura.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro no servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza uma fatura existente com base no nome informado.
        /// </summary>
        /// <returns>Mensagem de sucesso ou erro</returns>
        /// <response code="200">Fatura atualizada com sucesso</response>
        /// <response code="404">Fatura não encontrada</response>
        /// <response code="500">Erro na atualização da fatura</response>
        [HttpPut("{customer}")]
        public ActionResult UpdateInvoice(string customer, [FromBody] Invoice updatedInvoice)
        {
            var invoice = invoices.Find(i => i.Customer == customer);

            if (invoice == null)
            {
                return NotFound("Fatura não encontrada.");
            }

            invoice.Customer = updatedInvoice.Customer;
            invoice.Performances = updatedInvoice.Performances;

            var updated = invoices.Find(i => i.Customer == updatedInvoice.Customer && i.Performances == updatedInvoice.Performances);
            if (updated == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar a fatura.");
            }

            return Ok("Fatura atualizada com sucesso.");
        }

        /// <summary>
        /// Remove uma fatura com base no nome informado.
        /// </summary>
        /// <returns>Mensagem de sucesso ou erro</returns>
        /// <response code="200">Fatura removida com sucesso</response>
        /// <response code="404">Fatura não encontrada</response>
        /// <response code="500">Erro ao remover a fatura</response>
        [HttpDelete("{customer}")]
        public ActionResult DeleteInvoice(string customer)
        {
            var invoice = invoices.Find(i => i.Customer == customer);

            if (invoice == null)
            {
                return NotFound("Fatura não encontrada.");
            }

            invoices.Remove(invoice);

            var deleted = invoices.Find(i => i.Customer == customer);
            if (deleted != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover a fatura.");
            }

            return Ok("Fatura removida com sucesso.");
        }
    }
}