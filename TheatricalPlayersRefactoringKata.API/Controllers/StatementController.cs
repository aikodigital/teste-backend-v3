using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TheatricalPlayersRefactoringKata.Application.Services;
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
        private readonly ExtractService _extractService;

        public StatementController(EnqueueStatementUseCase enqueueStatementUseCase, GenerateStatementUseCase generateStatementUseCase, ExtractService extractService)
        {
            _enqueueStatementUseCase = enqueueStatementUseCase;
            _generateStatementUseCase = generateStatementUseCase;
            _extractService = extractService;
        }

        /// <summary>
        /// Gera um extrato - Teste com os PlayIds: "hamlet", "as-like", "othello", "henry-v" ou "richard-iii"
        /// </summary>
        /// <param name="invoice">Extrato a ser criado.</param>
        [HttpPost("GenerateExtract")]
        public async Task<IActionResult> GenerateExtract([FromBody] Invoice invoice)
        {
            var response = _generateStatementUseCase.GenerateExtractValues(invoice);

            // Save the extract in SQLite DB
            await _extractService.AddExtract(response);

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

