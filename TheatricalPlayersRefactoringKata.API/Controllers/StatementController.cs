using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TheatricalPlayersRefactoringKata.Application.UseCases;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("API responsável por operações de extrato.")]
    public class StatementController : ControllerBase
    {
        private readonly EnqueueStatementUseCase _enqueueStatementUseCase;
        private readonly GenerateStatementUseCase _generateStatementUseCase;

        public StatementController(EnqueueStatementUseCase enqueueStatementUseCase, GenerateStatementUseCase generateStatementUseCase)
        {
            _enqueueStatementUseCase = enqueueStatementUseCase;
            _generateStatementUseCase = generateStatementUseCase;
        }

        /// <summary>
        /// Gera um extrato
        /// </summary>
        /// <param name="invoice">Extrato a ser criado.</param>
        [HttpPost("GenerateExtract")]
        public IActionResult GenerateExtract([FromBody] Invoice invoice)
        {
            var response = _generateStatementUseCase.GenerateExtractValues(invoice);
            return Ok(response);
        }

        /// <summary>
        /// Enfileira um extrato para processamento.
        /// </summary>
        /// <param name="invoice">Extrato a ser enfileirado.</param>
        [HttpPost("enqueue")]
        public IActionResult EnqueueInvoice([FromBody] Invoice invoice)
        {
            _enqueueStatementUseCase.Execute(invoice);
            return Accepted("Invoice enqueued for processing.");
        }
    }
}

