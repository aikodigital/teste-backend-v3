using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        // In-memory volatile storage for statements [temporary]
        private static List<Statement> statements = new List<Statement>();

        // POST: api/statement/process
        // This endpoint queues an invoice for processing and returns immediately
        [HttpPost("process")]
        public IActionResult QueueInvoice([FromBody] InvoiceRequest request)
        {
            if (request.Invoice == null || request.Plays == null || request.Plays.Count == 0)
            {
                return BadRequest("Invalid invoice or plays data.");
            }

            // Create a new instance of StatementProcessor with the provided plays
            var processor = new StatementProcessor(request.Plays, "OutputDirectoryPath");

            // Queue and process the invoice
            processor.QueueInvoice(request.Invoice);
            processor.ProcessInvoicesAsync().Wait();

            // Generate the statement and store it in memory
            var statement = processor.ProcessInvoiceAsync(request.Invoice).Result;
            statements.Add(statement);

            return Ok("Invoice queued for processing and statement generated.");
        }

        // GET: api/statement/{statementId}
        // This endpoint retrieves a statement by its ID
        [HttpGet("{statementId}")]
        public IActionResult GetStatement(int statementId)
        {
            var statement = statements.Find(s => s.StatementID == statementId);
            if (statement == null)
            {
                return NotFound("Statement not found.");
            }
            return Ok(statement);
        }

        // GET: api/statement/{statementId}/xml
        // This endpoint retrieves a statement in XML format by its ID
        [HttpGet("{statementId}/xml")]
        public IActionResult GetStatementAsXml(int statementId)
        {
            var statement = statements.Find(s => s.StatementID == statementId);
            if (statement == null)
            {
                return NotFound("Statement not found.");
            }

            var xml = StatementPrinter.XmlPrint(statement);
            return Content(xml, "application/xml");
        }

        // GET: api/statements
        // This endpoint retrieves all statements in memory
        [HttpGet]
        public IActionResult GetAllStatements()
        {
            return Ok(statements);
        }
    }

    // Class to encapsulate Invoice and Plays in the request
    public class InvoiceRequest
    {
        public required Invoice Invoice { get; set; }
        public required Dictionary<string, Play> Plays { get; set; }
    }
}
