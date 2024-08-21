using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Data;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public StatementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("process")]
        public async Task<IActionResult> QueueInvoice([FromBody] InvoiceRequest request)
        {
            if (request.Invoice == null || request.Plays == null || request.Plays.Count == 0)
            {
                return BadRequest("Invalid invoice or plays data.");
            }

            // Create a new instance of StatementProcessor with the provided plays
            var processor = new StatementProcessor(request.Plays, "OutputDirectoryPath");

            // Queue and process the invoice without awaiting
            processor.QueueInvoice(request.Invoice); // No await here

            // Process invoices asynchronously
            await processor.ProcessInvoicesAsync();

            // Generate the statement
            var statement = await processor.ProcessInvoiceAsync(request.Invoice);

            // Store the statement in the database
            _context.Statements.Add(statement);
            await _context.SaveChangesAsync();

            return Ok("Invoice queued for processing and statement generated.");
        }

        // GET: api/statement/{statementId}
        [HttpGet("{statementId}")]
        public async Task<IActionResult> GetStatement(int statementId)
        {
            var statement = await _context.Statements.FindAsync(statementId);
            if (statement == null)
            {
                return NotFound("Statement not found.");
            }
            return Ok(statement);
        }

        // GET: api/statement/{statementId}/xml
        [HttpGet("{statementId}/xml")]
        public async Task<IActionResult> GetStatementAsXml(int statementId)
        {
            var statement = await _context.Statements.FindAsync(statementId);
            if (statement == null)
            {
                return NotFound("Statement not found.");
            }

            var xml = StatementPrinter.XmlPrint(statement);
            return Content(xml, "application/xml");
        }

        // GET: api/statements
        [HttpGet]
        public async Task<IActionResult> GetAllStatements()
        {
            var statements = await _context.Statements.ToListAsync();
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
