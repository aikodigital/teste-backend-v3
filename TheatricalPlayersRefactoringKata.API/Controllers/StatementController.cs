using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.UseCases;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly EnqueueStatementUseCase _enqueueStatementUseCase;
        private readonly GenerateStatementUseCase _generateStatementUseCase;

        public StatementController(EnqueueStatementUseCase enqueueStatementUseCase, GenerateStatementUseCase generateStatementUseCase)
        {
            _enqueueStatementUseCase = enqueueStatementUseCase;
            _generateStatementUseCase = generateStatementUseCase;
        }

        [HttpPost("GenerateExtract")]
        public IActionResult GenerateExtract([FromBody] Invoice invoice)
        {
            var response = _generateStatementUseCase.GenerateExtractValues(invoice);
            return Ok(response);
        }

        [HttpPost("enqueue")]
        public IActionResult EnqueueInvoice([FromBody] Invoice invoice)
        {
            _enqueueStatementUseCase.Execute(invoice);
            return Accepted("Invoice enqueued for processing.");
        }
    }
}

