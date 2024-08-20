using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        // Inject the StatementProcessor service
        private readonly StatementProcessor _statementProcessor;

        // Constructor to inject the StatementProcessor service
        public StatementController(StatementProcessor statementProcessor)
        {
            _statementProcessor = statementProcessor;
        }

        // In-memory volatile storage for statements [temporary]
        private static List<Statement> statements = new List<Statement>();

        // POST: api/statement/process
        [HttpPost("process")]
        public IActionResult QueueInvoice([FromBody] Invoice invoice)
        {
            if (invoice == null)
            {
                return BadRequest("Invalid invoice data.");
            }

            // Add invoice to queue
            _statementProcessor.QueueInvoice(invoice);
            
            // Return immediately, processing happens asynchronously
            return Ok("Invoice queued for processing.");
        }

        // Endpoint to trigger processing of all enqueued invoices
        [HttpPost("process/all")]
        public async Task<IActionResult> ProcessAllInvoices()
        {
            await _statementProcessor.ProcessInvoicesAsync();
            return Ok("All queued invoices have been processed.");
        }

        // GET: api/statement/{statementId}
        [HttpGet("{statementId}")]
        public IActionResult GetStatement(int statementId)
        {
            var statement = statements.Find(s => s.StatementID == statementId);
            if (statement == null)
            {
                return NotFound("Statement not found");
            }
            return Ok(statement);
        }

        // GET: api/statement/{statementId}/xml
        [HttpGet("{statementId}/xml")]
        public IActionResult GetStatementAsXml(int statementId)
        {
            var statement = statements.Find(s => s.StatementID == statementId);
            if (statement == null)
            {
                return NotFound("Statement not found");
            }

            var xml = StatementPrinter.XmlPrint(statement);
            return Content(xml, "application/xml");
        }

        // GET: api/statements
        [HttpGet]
        public IActionResult GetAllStatements()
        {
            return Ok(statements);
        }
    }
}
