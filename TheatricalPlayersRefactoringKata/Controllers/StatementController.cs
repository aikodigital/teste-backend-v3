using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Dtos;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Services.Interface;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IStatementPrinterService _statementPrinter;

        // Constructor injecting the necessary dependencies
        public StatementController(IStatementPrinterService statementPrinter, ApplicationDbContext context, IMapper mapper)
        {
            _statementPrinter = statementPrinter;
            _context = context;
            _mapper = mapper;
        }

        // POST: Generate a statement in plain text
        [HttpPost("GenerateStatement")]
        public IActionResult GenerateStatement([FromBody] InvoiceDto invoiceDto)
        {
            // Map DTO to Invoice entity
            var invoice = _mapper.Map<Invoice>(invoiceDto);

            // Get the list of plays from a predefined dictionary
            var plays = GetPlaysDictionary();

            // Generate the statement in text format
            var result = _statementPrinter.Print(invoice, plays);

            // Return the generated statement
            return Ok(result);
        }

        // POST: Generate a statement in XML format
        [HttpPost("GenerateStatementXml")]
        public IActionResult GenerateStatementXml([FromBody] InvoiceDto invoiceDto)
        {
            // Map DTO to Invoice entity
            var invoice = _mapper.Map<Invoice>(invoiceDto);

            // Get the list of plays from a predefined dictionary
            var plays = GetPlaysDictionary();

            // Generate the statement in XML format
            var result = _statementPrinter.PrintAsXml(invoice, plays);

            // Return the generated XML statement
            return Ok(result);
        }

        // Helper method to return a predefined list of plays
        private Dictionary<string, Play> GetPlaysDictionary()
        {
            return new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };
        }
    }
}
